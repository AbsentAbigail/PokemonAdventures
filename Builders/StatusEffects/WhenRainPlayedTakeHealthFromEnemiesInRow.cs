using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.FieldEffects;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class WhenRainPlayedTakeHealthFromEnemiesInRow : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenCertainCardPlayed>(Name)
            .WithText($"When {Mod.CardTag(HarshSunlight.Name)} triggers, take <{{a}}><keyword=health> from enemies in row")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenCertainCardPlayed>(status =>
            {
                status.effectToApply = Mod.GetStatus("Take Health");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.EnemiesInRow;
                status.cardConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintIsSpecificCard>("Is Sun", constraint => constraint.allowedCards = [Mod.GetCard(HarshSunlight.Name)]),
                ];
                status.onlyAllied = false;
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}