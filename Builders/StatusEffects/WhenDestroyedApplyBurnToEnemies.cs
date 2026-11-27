using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class WhenDestroyedApplyBurnToEnemies : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenDestroyed>(Name)
            .WithText($"When destroyed, apply <{{a}}>{Mod.KeywordTag(Keywords.Burn.Name)} to all enemies")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDestroyed>(status =>
            {
                status.effectToApply = Mod.GetStatus(Burn.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Enemies;
                status.targetMustBeAlive = false;
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}