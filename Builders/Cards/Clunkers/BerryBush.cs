using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Clunkers;

[UsedImplicitly]
public class BerryBush : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Berry Bush")
            .WithCardType("Clunker")
            .SetStats(null, null, 4)
            .SetSprites(
                Mod.GetSprite("BerryBush"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(20)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack("Scrap", 2),
                    Mod.SStack(OnCardPlayedAddRandomBerryToHand.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}