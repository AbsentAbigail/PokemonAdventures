using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;
using PokemonMod.Variables;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenPsychicAllyHitApplySleepToEveryone : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenAllyIsHitWithConstraint>(Name)
            .WithText($"When a <sprite name={Types.Psychic.Keyword()}> ally is hit, apply <{{a}}>{Mod.KeywordTag(Keywords.Sleep.Name)} to everyone")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenAllyIsHitWithConstraint>(status =>
            {
                status.effectToApply = Mod.GetStatus(Sleep.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies | StatusEffectApplyX.ApplyToFlags.Enemies | StatusEffectApplyX.ApplyToFlags.Self;
                status.constraints =
                [
                    TargetConstraintHelper.HasStatus(Types.Psychic.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}