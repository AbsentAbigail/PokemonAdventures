using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Granbull : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Granbull")
            .SetStats(9, 6, 5)
            .SetSprites(
                Mod.GetSprite("Granbull"),
                Mod.GetBackgroundSprite(BackgroundSprites.Moonflowers))
            .DropsBling(7)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Fairy.Name),
                    Mod.SStack(WhenDeployedApplyFrostToEnemies.Name, 2),
                    Mod.SStack(WhenDebuffedCountDown.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}