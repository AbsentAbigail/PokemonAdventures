using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenDebuffedConstrictApplierAndCleanseSelf : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenDebuffed>(Name)
            .WithText($"When debuffed, apply <{{a}}>{Mod.KeywordTag(Keywords.Constricted.Name)} to applier and <keyword=cleanse> self")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDebuffed>(status =>
            {
                status.effectToApply = Mod.GetStatus(InstantMultipleConstrictAndCleanseApplier.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}