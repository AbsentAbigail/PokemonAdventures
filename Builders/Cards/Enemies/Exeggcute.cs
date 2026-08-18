using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Exeggcute : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Exeggcute")
            .WithCardType("Enemy")
            .SetStats(6, 1, 3)
            .SetSprites(
                Mod.GetSprite("Exeggcute"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(4)
            .WithText("I don't evolve yet :(")
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Grass.Name),
                    Mod.SStack(Types.Psychic.Name),
                    Mod.SStack(WhenBerryConsumedAddCopyToDiscardPile.Name),
                    Mod.SStack(OnCardPlayedHealEqualToSun.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}