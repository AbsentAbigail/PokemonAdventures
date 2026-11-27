using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;
using PokemonMod.Variables;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class WhenDragonWaterGhostSteelRecalledMoveThemToHand : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenCardRecalled>(Name)
            .WithText(
                $"When a <sprite name={Types.Dragon.Keyword()}>, <sprite name={Types.Steel.Keyword()}>, <sprite name={Types.Water.Keyword()}>, or <sprite name={Types.Ghost.Keyword()}> Pokemon is recalled, return them to hand")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenCardRecalled>(status =>
            {
                status.effectToApply = Mod.GetStatus(InstantMoveToHand.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Target;
                status.allies = true;
                status.enemies = true;
                status.applyConstraints =
                [
                    TargetConstraintHelper.IsOfTypes([
                        Types.Dragon.Name,
                        Types.Steel.Name,
                        Types.Water.Name,
                        Types.Ghost.Name,
                    ]),
                ];
                status.actionPriority = -1;
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}