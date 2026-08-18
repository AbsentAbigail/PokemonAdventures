using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Remoraid : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Remoraid")
            .WithCardType("Enemy")
            .SetStats(2, 2, 3)
            .SetSprites(
                Mod.GetSprite("Remoraid"),
                Mod.GetBackgroundSprite(BackgroundSprites.Ocean))
            .DropsBling(5)
            // .EvolvesInto(Name)
            .WithText("I don't evolve yet :(")
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Water.Name),
                    Mod.SStack("While Active Increase Attack To AlliesInRow"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}