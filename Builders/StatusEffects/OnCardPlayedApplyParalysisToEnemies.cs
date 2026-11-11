using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class OnCardPlayedApplyParalysisToEnemies : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXOnCardPlayed>(Name)
            .WithText($"Apply <{{a}}>{Mod.KeywordTag(Keywords.Paralysis.Name)} to all enemies")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnCardPlayed>(status =>
            {
                status.effectToApply = Mod.GetStatus(Paralysis.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Enemies;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}