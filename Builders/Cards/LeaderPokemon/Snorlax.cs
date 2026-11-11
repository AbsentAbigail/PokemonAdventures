using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Builders.Traits;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.LeaderPokemon;

[UsedImplicitly]
public class Snorlax : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Snorlax")
            .SetStats(10, 6, 3)
            .SetSprites(
                Mod.GetSprite("Snorlax"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Normal.Name),
                    Mod.SStack(Sleep.Name, 6),
                    Mod.SStack(WhenBerryPlayedOnSelfApplyFrenzy.Name),
                    Mod.SStack(WhenEatingBerryRestoreOwnHealth.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}