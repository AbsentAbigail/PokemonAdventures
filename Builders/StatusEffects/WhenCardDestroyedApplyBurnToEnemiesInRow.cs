using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class WhenCardDestroyedApplyBurnToEnemiesInRow : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenCardDestroyed>(Name)
            .WithText($"When a card is destroyed, apply <{{a}}>{Mod.KeywordTag(Keywords.Burn.Name)} to enemies in the row")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenCardDestroyed>(status =>
            {
                status.effectToApply = Mod.GetStatus(Burn.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.EnemiesInRow;
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}