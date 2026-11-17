using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Clunkers;

[UsedImplicitly]
public class MtMoon : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Mt. Moon")
            .WithCardType("Clunker")
            .SetStats(null, null, 5)
            .SetSprites(
                Mod.GetSprite("MtMoon"),
                Mod.GetBackgroundSprite(BackgroundSprites.Moonflowers))
            .WithValue(35)
            .InItemPool()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack("Scrap", 2),
                    Mod.SStack(OnCardPlayedIncreaseAttackOfAllies.Name),
                    Mod.SStack(WhenAnythingSleepdCountDown.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}