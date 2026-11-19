using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class WhenBerryPlayedApplyShroomParalysisSleepToEnemiesInRow : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenCertainCardPlayed>(Name)
            .WithText(
                $"When a {Mod.KeywordTag(Berry.Name)} is played, apply <{{a}}><keyword=shroom>, <{{a}}>{Mod.KeywordTag(Keywords.Paralysis.Name)} and <{{a}}>{Mod.KeywordTag(Keywords.Sleep.Name)} to enemies in row")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenCertainCardPlayed>(status =>
            {
                status.effectToApply = Mod.GetStatus(InstantMultipleShroomParalysisSleep.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.EnemiesInRow;
                status.cardConstraints =
                [
                    TargetConstraintHelper.HasTrait(Traits.Berry.Name),
                ];
                status.onlyAllied = false;
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}