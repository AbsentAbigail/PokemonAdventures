using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenShellLostGainEqualAttack : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenYLost>(Name)
            .WithText("When <keyword=shell> is lost, gain equal <keyword=attack>")
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenYLost>(status =>
            {
                status.statusType = Mod.GetStatus("Shell").type;
                status.effectToApply = Mod.GetStatus("Increase Attack");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.applyEqualAmount = true;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}