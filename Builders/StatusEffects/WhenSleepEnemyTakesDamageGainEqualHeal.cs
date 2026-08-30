using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenSleepEnemyTakesDamageGainEqualHeal : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenCardTakesDamage>(Name)
            .WithText($"Whenever a {Mod.KeywordTag(Keywords.Sleep.Name)}'d enemy takes damage, restore equal <keyword=health>")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenCardTakesDamage>(status =>
            {
                status.effectToApply = Mod.GetStatus("Heal");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.allies = false;
                status.self = false;
                status.constraints =
                [
                    TargetConstraintHelper.HasStatus(Sleep.Name),
                ];
                status.applyEqualAmount = true;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}