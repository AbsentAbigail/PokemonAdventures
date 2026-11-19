using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Gloom : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Gloom")
            .WithCardType("Enemy")
            .SetStats(6, 1, 3)
            .SetSprites(
                Mod.GetSprite("Gloom"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(6)
            .EvolvesInto(Vileplume.Name)
            .EvolvesInto(Bellossom.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Grass.Name),
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack(OnCardPlayedAddRandomBerryToHand.Name),
                    Mod.SStack(WhenBerryPlayedApplyShroomParalysisToEnemiesInRow.Name),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}