using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Traits;
using PokemonMod.Helpers;
using PokemonMod.Scriptables.ScriptableAmounts;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class InstantApplyParalysisEqualToDebuffs : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXInstant>(Name)
            .WithText($"Apply {Mod.KeywordTag(Keywords.Paralysis.Name)} equal target's debuffs")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXInstant>(status =>
            {
                status.effectToApply = Mod.GetStatus(Paralysis.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.scriptableAmount = new Script<ScriptableDebuffs>();
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}