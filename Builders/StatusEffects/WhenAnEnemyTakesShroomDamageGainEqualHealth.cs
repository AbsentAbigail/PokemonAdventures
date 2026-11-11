using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenAnEnemyTakesShroomDamageGainEqualHealth : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenAnEnemyTakesDamage>(Name)
            .WithText("Whenever an enemy takes damage from <keyword=shroom>, gain equal <keyword=health>")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenAnEnemyTakesDamage>(status =>
            {
                status.effectToApply = Mod.GetStatus("Increase Max Health");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.applyEqualAmount = true;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}