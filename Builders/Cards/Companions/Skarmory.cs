using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Skarmory : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Skarmory")
            .SetStats(8, 2, 4)
            .SetSprites(
                Mod.GetSprite("Skarmory"),
                Mod.GetBackgroundSprite(BackgroundSprites.Sky))
            .DropsBling(4)
            .InUnitPool()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Steel.Name),
                    Mod.SStack(Types.Flying.Name),
                    Mod.SStack(WhenEnemyEntersBattleDealDamage.Name, 3),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}