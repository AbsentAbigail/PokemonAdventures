using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenEnemyEntersBattleApplyShroom : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenEnemyEntersBattle>(Name)
            .WithText("When an enemy enters battle apply <{a}><keyword=shroom> to them")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenEnemyEntersBattle>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Target;
                status.effectToApply = Mod.GetStatus("Shroom");
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}