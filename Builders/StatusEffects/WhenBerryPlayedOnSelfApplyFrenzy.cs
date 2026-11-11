using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Traits;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenBerryPlayedOnSelfApplyFrenzy : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXBeforeCardTrigger>(Name)
            .WithText("Add <x{a}><keyword=frenzy> to <Berries> played on me")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXBeforeCardTrigger>(status =>
            {
                status.effectToApply = Mod.GetStatus("MultiHit");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
                status.cardConstraints =
                [
                    TargetConstraintHelper.HasTrait(Berry.Name),
                ];
                status.onlyAllied = false;
                status.onSelf = true;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}