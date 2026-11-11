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
public class WhenBerryPlayedTriggerAllies : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenCertainCardPlayed>(Name)
            .WithText($"When a {Mod.KeywordTag(Berry.Name)} is played, trigger all {Mod.KeywordTag(Types.Bug.Keyword())} allies")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenCertainCardPlayed>(status =>
            {
                status.effectToApply = Mod.GetStatus("Trigger (High Prio)");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
                status.cardConstraints =
                [
                    TargetConstraintHelper.HasTrait(Traits.Berry.Name),
                ];
                status.onlyAllied = false;
                status.applyConstraints =
                [
                    TargetConstraintHelper.IsOfTypes([Types.Bug.Name]),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}