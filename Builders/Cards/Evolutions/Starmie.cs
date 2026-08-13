using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Evolutions;

[UsedImplicitly]
public class Starmie : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Starmie")
            .WithCardType("Enemy")
            .SetStats(5, 1, 3)
            .SetSprites(
                Mod.GetSprite("Starmie"),
                Mod.GetBackgroundSprite(BackgroundSprites.Ocean))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Water.Name),
                    Mod.SStack(Types.Psychic.Name),
                    Mod.SStack(WhileActiveFrenzyEqualToAlliesAndEnemiesInRow.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}