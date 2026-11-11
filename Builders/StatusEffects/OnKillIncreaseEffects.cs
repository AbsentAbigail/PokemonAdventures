using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class OnKillIncreaseEffects : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXOnKill>(Name)
            .WithText("On kill, increase effects by {a}")
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnKill>(status =>
            {
                status.effectToApply = Mod.GetStatus("Ongoing Increase Effects");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}