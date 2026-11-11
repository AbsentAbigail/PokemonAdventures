using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Spinarak : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Spinarak")
            .WithCardType("Enemy")
            .SetStats(3, 1, 5)
            .SetSprites(
                Mod.GetSprite("Spinarak"),
                Mod.GetBackgroundSprite(BackgroundSprites.Pollution))
            .DropsBling(5)
            .EvolvesInto(Ariados.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack("Shroom", 2),
                ];
                card.startWithEffects =
                [
                    Mod.SStack(Types.Bug.Name),
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack(WhenAnEnemyTakesShroomDamageGainEqualHealth.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}