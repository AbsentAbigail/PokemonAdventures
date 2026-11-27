using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class OnCardPlayedApplyOverloadToEnemiesInRow : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXOnCardPlayed>(Name)
            .WithText("Apply <{a}><keyword=overload> to enemies in row")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnCardPlayed>(status =>
            {
                status.effectToApply = Mod.GetStatus("Overload");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.EnemiesInRow;
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}