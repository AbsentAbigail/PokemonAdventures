using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenWaterOrIceAllyDeployedApplyLumin : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenDeployed>(Name)
            .WithText($"When a <sprite name={Types.Water.Keyword()}> or <sprite name={Types.Ice.Keyword()}> ally is deployed, apply <keyword=lumin> to it")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDeployed>(status =>
            {
                status.whenAllyDeployed = true;
                status.whenSelfDeployed = false;
                status.effectToApply = Mod.GetStatus("Lumin");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Target;
                status.eventPriority = +100;
                status.applyConstraints =
                [
                    TargetConstraintHelper.Or("Ice or Water",
                        not: false,
                        TargetConstraintHelper.HasStatus(Types.Water.Name),
                        TargetConstraintHelper.HasStatus(Types.Ice.Name)
                    ),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}