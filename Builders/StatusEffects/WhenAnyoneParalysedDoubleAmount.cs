using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenAnyoneParalysedDoubleAmount : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenYAppliedTo>(Name)
            .WithText($"Whenever anything is {Mod.KeywordTag(Keywords.Paralysis.Name)}'d, double the amount")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenYAppliedTo>(status =>
            {
                status.adjustAmount = true;
                status.multiplyAmount = 2;
                status.whenAppliedTypes = ["paralysis"];
                status.whenAppliedToFlags = StatusEffectApplyX.ApplyToFlags.Self | StatusEffectApplyX.ApplyToFlags.Allies | StatusEffectApplyX.ApplyToFlags.Enemies;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}