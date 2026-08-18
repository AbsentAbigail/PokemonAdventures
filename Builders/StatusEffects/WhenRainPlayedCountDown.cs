using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.FieldEffects;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;
using PokemonMod.Variables;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenRainPlayedCountDown : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenCertainCardPlayed>(Name)
            .WithText($"When {Mod.CardTag(Rain.Name)} triggers, count down own <keyword=counter> by <{{a}}>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenCertainCardPlayed>(status =>
            {
                status.effectToApply = Mod.GetStatus("Reduce Counter");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.cardConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintIsSpecificCard>("Is Rain", constraint => constraint.allowedCards = [Mod.GetCard(Rain.Name)]),
                ];
                status.allies = false;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}