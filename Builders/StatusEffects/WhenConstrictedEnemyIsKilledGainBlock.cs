using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenConstrictedEnemyIsKilledGainBlock : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenUnitIsKilled>(Name)
            .WithText($"When a {Mod.KeywordTag(Keywords.Constricted.Name)}'d enemy dies, gain <{{a}}><keyword=block>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenUnitIsKilled>(status =>
            {
                status.effectToApply = Mod.GetStatus("Block");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.unitConstraints = [TargetConstraintHelper.HasStatus(Constricted.Name)];
                status.ally = false;
                status.enemy = true;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}