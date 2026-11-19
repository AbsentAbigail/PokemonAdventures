using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Clunkers;

[UsedImplicitly]
public class SinjohRuins : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Sinjoh Ruins")
            .WithCardType("Clunker")
            .SetStats()
            .SetSprites(
                Mod.GetSprite("SinjohRuins"),
                Mod.GetBackgroundSprite(BackgroundSprites.Cave))
            .WithValue(35)
            .InItemPool()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack("Scrap", 2),
                    Mod.SStack(WhenDragonWaterGhostSteelRecalledMoveThemToHand.Name),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}