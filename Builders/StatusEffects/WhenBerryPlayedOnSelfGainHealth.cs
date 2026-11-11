using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;
using PokemonMod.Variables;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenBerryPlayedOnSelfGainHealth : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenCertainCardPlayed>(Name)
            .WithText($"When eating a {Mod.KeywordTag(Berry.Name)}, gain <+{{a}}><keyword=health>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenCertainCardPlayed>(status =>
            {
                status.effectToApply = Mod.GetStatus("Increase Max Health");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.cardConstraints =
                [
                    TargetConstraintHelper.HasTrait(Traits.Berry.Name),
                ];
                status.onlyAllied = false;
                status.onSelf = true;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}