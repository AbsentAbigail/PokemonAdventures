using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class OnCardPlayedSwapOwnAttackAndCounterPlus : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXAfterCardPlayed>(Name)
            .WithText("""
                      Lose 1<keyword=attack>, gain 2<keyword=counter>
                      Then swap own <keyword=attack> and <keyword=counter>
                      """)
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXAfterCardPlayed>(status =>
            {
                status.effectToApply = Mod.GetStatus(InstantSwapAttackAndCounterPlus.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}