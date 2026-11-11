using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenDebuffedCountDown : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenDebuffed>(Name)
            .WithText($"When debuffed, count down own <keyword=counter> by <{{a}}>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDebuffed>(status =>
            {
                status.effectToApply = Mod.GetStatus("Reduce Counter");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}