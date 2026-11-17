using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class MistyAndStarmie2 : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Misty and Starmie")
            .WithCardType("Boss")
            .SetStats(14, 1, 3)
            .SetSprites(
                Mod.GetSprite("MistyAndStarmie2"),
                Mod.GetBackgroundSprite(BackgroundSprites.Ocean))
            .DropsBling(27)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Water.Name),
                    Mod.SStack(Types.Psychic.Name),
                    Mod.SStack(WhileActiveFrenzyEqualToAlliesAndEnemies.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}