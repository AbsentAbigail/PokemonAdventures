using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhileActiveDecreaseAttackOfEnemies : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectWhileActiveX>(Name)
            .WithText("While active, reduce <keyword=attack> of all enemies by <{a}>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectWhileActiveX>(status =>
            {
                status.effectToApply = Mod.GetStatus("Ongoing Reduce Attack");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Enemies;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}