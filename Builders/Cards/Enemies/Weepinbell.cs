using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Weepinbell : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Weepinbell")
            .WithCardType("Enemy")
            .SetStats(6, 3, 5)
            .SetSprites(
                Mod.GetSprite("Weepinbell"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(7)
            .EvolvesInto(Victreebel.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Grass.Name),
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack(WhenHealthLostConsumeRightmostBerriesInHand.Name),
                    Mod.SStack(WhenBerryConsumedSunnyCountDownCounter.Name, 2),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}