using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class InstantApplyOnCardPlayedHealSelf : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectInstantApplyEffectAndUpdate>(Name)
            .WithText("The target gains <{a}> restore own health")
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantApplyEffectAndUpdate>(status =>
            {
                status.effectToApply = Mod.GetStatus(OnCardPlayedHealSelf.Name);
                status.targetConstraints =
                [
                    TargetConstraintHelper.DoesTrigger(),
                    TargetConstraintHelper.HealthMoreThan(0),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}