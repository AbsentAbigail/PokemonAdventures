using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Victreebel : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Victreebel")
            .WithCardType("Enemy")
            .SetStats(8, 4, 5)
            .SetSprites(
                Mod.GetSprite("Victreebel"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(9)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Grass.Name),
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack(WhenHealthLostConsumeBerriesInHand.Name),
                    Mod.SStack(WhenBerryConsumedSunnyCountDownCounter.Name, 2),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}