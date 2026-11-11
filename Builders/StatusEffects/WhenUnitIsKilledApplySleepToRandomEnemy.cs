using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenUnitIsKilledApplySleepToRandomEnemy : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenUnitIsKilled>(Name)
            .WithText($"When another card is killed, apply <{{a}}>{Mod.KeywordTag(Keywords.Sleep.Name)} to a random enemy")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenUnitIsKilled>(status =>
            {
                status.effectToApply = Mod.GetStatus(Sleep.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.RandomEnemy;
                status.enemy = true;
                status.queue = true;
                status.applyConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintIsAlive>(),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}