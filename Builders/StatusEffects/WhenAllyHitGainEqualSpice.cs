using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class WhenAllyHitGainEqualSpice : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenAllyIsHitEqualAmount>(Name)
            .WithText("When an ally is hit, gain <keyword=spice> equal to damage taken")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenAllyIsHitEqualAmount>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.effectToApply = Mod.GetStatus("Spice");
                status.applyEqualAmount = true;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}