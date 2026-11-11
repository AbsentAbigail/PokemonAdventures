using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenHitApplyShroomToEnemies : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenHit>(Name)
            .WithText("When hit, apply <{a}><keyword=shroom> to all enemies")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenHit>(status =>
            {
                status.effectToApply = Mod.GetStatus("Shroom");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Enemies;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}