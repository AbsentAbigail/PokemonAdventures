using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class InstantMultipleConstrictAndCleanseApplier : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectInstantMultiple>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantMultiple>(status =>
            {
                status.applyXEffects = [
                    Mod.GetStatusOf<StatusEffectApplyXInstant>(InstantCleanseApplier.Name),
                ];
                status.effects =
                [
                    Mod.GetStatusOf<StatusEffectInstantApplyEffect>(InstantApplyConstrict.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}