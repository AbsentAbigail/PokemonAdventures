using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Horsea : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Horsea")
            .WithCardType("Enemy")
            .SetStats(5, 2, 4)
            .SetSprites(
                Mod.GetSprite("Horsea"),
                Mod.GetBackgroundSprite(BackgroundSprites.Ocean))
            .DropsBling(5)
            .WithText("I don't evolve yet :(")
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Water.Name),
                    Mod.SStack(OnCardPlayedSwapOwnAttackAndCounter.Name),
                    Mod.SStack(WhenRainPlayedCountDown.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}