using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Shellder : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Shellder")
            .WithCardType("Enemy")
            .SetStats(1, 1, 5)
            .SetSprites(
                Mod.GetSprite("Shellder"),
                Mod.GetBackgroundSprite(BackgroundSprites.Ocean))
            .DropsBling(4)
            // .EvolvesInto()
            .WithText("I don't evolve yet :(")
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Water.Name),
                    Mod.SStack("Shell", 6),
                    Mod.SStack("When Hit Apply Snow To Attacker"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}