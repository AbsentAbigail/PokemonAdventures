using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class ConstrictApplyInk : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenConstrictedDamageDealt>(Name)
            .WithText($"When an enemy takes damage from {Mod.KeywordTag(Keywords.Constricted.Name)}, apply equal <keyword=null>")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenConstrictedDamageDealt>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Target;
                status.effectToApply = Mod.GetStatus("Null");
                status.onlyEnemies = true;
                status.applyEqualAmount = true;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}