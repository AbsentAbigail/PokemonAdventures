using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Ampharos : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Ampharos")
            .SetStats(7, 0, 6)
            .SetSprites(
                Mod.GetSprite("Ampharos"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack("Weakness", 6),
                ];
                card.startWithEffects =
                [
                    Mod.SStack(Types.Electric.Name),
                    Mod.SStack(WhenEnemyIsBomdDealDamage.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}