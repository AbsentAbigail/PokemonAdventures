using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class OnCardPlayedApplyInkToConstrictedEnemies : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXOnCardPlayed>(Name)
            .WithText($"Apply <{{a}}><keyword=null> to all {Mod.KeywordTag(Keywords.Constricted.Name)} enemies")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnCardPlayed>(status =>
            {
                status.effectToApply = Mod.GetStatus("Null");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Enemies;
                status.applyConstraints =
                [
                    TargetConstraintHelper.HasStatus(Constricted.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}