using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Items;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class InstantDealDamageEqualToSleep : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectInstantDealDamage>(Name)
            .WithText($"Deal additional damage equal to targets {Mod.KeywordTag(Keywords.Sleep.Name)}")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantDealDamage>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;

                status.dealDamage = true;
                status.countsAsHit = true;

                status.scriptableAmount = new Script<ScriptableCurrentStatus>(script => script.statusType = "sleep");
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}