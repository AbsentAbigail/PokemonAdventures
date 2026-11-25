using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Sunflora : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Sunflora")
            .WithCardType("Enemy")
            .SetStats(4, 1, 4)
            .SetSprites(
                Mod.GetSprite("Sunflora"),
                Mod.GetBackgroundSprite(BackgroundSprites.FlowerPark))
            .DropsBling(5)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Grass.Name),
                    Mod.SStack(OnCardPlayedLoseHealth.Name),
                    Mod.SStack("On Card Played Apply Attack To Self", 2),
                    Mod.SStack(WhenRainPlayedTakeHealthFromEnemiesInRow.Name),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}