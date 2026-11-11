using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenEnemyIsSleepedRestoreHealth : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXToTargetWhenYApplied>(Name)
            .WithText($"When an enemy is {Mod.KeywordTag(Keywords.Sleep.Name)}'d, restore <{{a}}><keyword=health>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXToTargetWhenYApplied>(status =>
            {
                status.whenAppliedTypes =
                [
                    "sleep",
                ];
                status.whenAppliedToFlags = StatusEffectApplyX.ApplyToFlags.Enemies;
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.effectToApply = Mod.GetStatus("Heal");
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}