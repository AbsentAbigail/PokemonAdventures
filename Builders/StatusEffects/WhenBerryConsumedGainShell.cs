using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.FieldEffects;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenBerryConsumedGainShell : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenCardConsumed>(Name)
            .WithText($"When a {Mod.KeywordTag(Berry.Name)} is consumed, gain <{{a}}><keyword=shell>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenCardConsumed>(status =>
            {
                status.effectToApply = Mod.GetStatus("Shell");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.mustBeOnBoard = false;
                status.constraints =
                [
                    TargetConstraintHelper.HasTrait(Traits.Berry.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}