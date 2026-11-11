using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenAnythingSleepdDealDamage : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXToTargetWhenYApplied>(Name)
            .WithText($"When anything is {Mod.KeywordTag(Keywords.Sleep.Name)}'d, deal <{{a}}> damage to them")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXToTargetWhenYApplied>(status =>
            {
                status.whenAppliedTypes =
                [
                    "sleep",
                ];
                status.whenAppliedToFlags = StatusEffectApplyX.ApplyToFlags.Allies | StatusEffectApplyX.ApplyToFlags.Enemies | StatusEffectApplyX.ApplyToFlags.Self;
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Target;
                status.effectToApply = Mod.GetStatus(InstantDealDamage.Name);
                status.doesDamage = true;
                status.queue = true;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}