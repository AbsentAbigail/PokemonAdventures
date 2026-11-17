using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class ErikaAndVileplume2 : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Erika and Vileplume")
            .WithCardType("Boss")
            .SetStats(20, 2, 4)
            .SetSprites(
                Mod.GetSprite("ErikaAndVileplume2"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(27)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack("Shroom"),
                ];
                card.startWithEffects =
                [
                    Mod.SStack(Types.Grass.Name),
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack(WhenBerryPlayedApplyParalysisToEnemiesAndIncreaseEffects.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}