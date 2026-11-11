using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.FieldEffects;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenSandstormPlayedCountDownAlliedPokemon : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenCertainCardPlayed>(Name)
            .WithText($"When {Mod.CardTag(Sandstorm.Name)} triggers, count down <keyword=counter> of allied Pokemon by <{{a}}>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenCertainCardPlayed>(status =>
            {
                status.effectToApply = Mod.GetStatus("Reduce Counter");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
                status.cardConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintIsSpecificCard>("Is Sandstorm", constraint => constraint.allowedCards = [Mod.GetCard(Sandstorm.Name)]),
                ];
                status.applyConstraints =
                [
                    TargetConstraintHelper.IsPokemon(),
                ];
                status.onlyAllied = false;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}