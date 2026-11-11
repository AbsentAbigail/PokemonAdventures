using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.FieldEffects;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenRainPlayedTrigger : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenCertainCardPlayed>(Name)
            .WithText($"Trigger when {Mod.CardTag(Rain.Name)} triggers")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .IsReaction()
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenCertainCardPlayed>(status =>
            {
                status.effectToApply = Mod.GetStatus("Trigger (High Prio)");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.cardConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintIsSpecificCard>("Is Rain", constraint => constraint.allowedCards = [Mod.GetCard(Rain.Name)]),
                ];
                status.onlyAllied = false;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}