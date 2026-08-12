using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.LeaderPokemon;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using UnityEngine;

namespace PokemonMod.Builders.Cards.Leaders;

[UsedImplicitly]
public class Blue : ITrainerBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Blue")
            .SetStats(5, 0, 4)
            .SetSprites(
                Mod.GetSprite("Blue"),
                Mod.GetBackgroundSprite(BackgroundSprites.Sky))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack("Frost", 2),
                ];
                card.startWithEffects =
                [
                    Mod.SStack(WhenWaterAllyHitApplyShell.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public ITrainerBuilder.MenuTrainerModifier MenuTrainerModifiers => new()
    {
        Partners = [Squirtle.Name],
        MenuSpriteName = "BlueAndSquirtle",
        MenuTitle = "Blue and Squirtle",
    };
}