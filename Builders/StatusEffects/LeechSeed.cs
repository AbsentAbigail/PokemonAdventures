using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using WildfrostHopeMod.VFX;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class LeechSeed : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXEveryTurn>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXEveryTurn>(status =>
            {
                status.effectToApply = Mod.GetStatus(InstantTakeHpFromApplier.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Applier;
                status.targetConstraints =
                [
                    TargetConstraintHelper.HealthMoreThan(0),
                ];
                
                status.type = "leechseed";
                status.offensive = true;
                status.removeOnDiscard = true;
            })
            .Subscribe_WithStatusIcon("leechseed");
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}