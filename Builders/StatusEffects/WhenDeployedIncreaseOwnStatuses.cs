using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenDeployedIncreaseOwnStatuses : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenDeployed>(Name)
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDeployed>(status =>
            {
                status.effectToApply = Mod.GetStatus(InstantCountUpStatuses.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}