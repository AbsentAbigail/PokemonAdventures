using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Scriptables.TargetConstraints;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class WhileActiveFullyEvolvedPokemonGainAttack : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectWhileActiveX>(Name)
            .WithText("While active, fully evolved Pokemon have <+{a}><keyword=attack>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectWhileActiveX>(status =>
            {
                status.effectToApply = Mod.GetStatus("Ongoing Increase Attack");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies | StatusEffectApplyX.ApplyToFlags.Enemies;
                status.applyConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintCanEvolve>(not: true),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}