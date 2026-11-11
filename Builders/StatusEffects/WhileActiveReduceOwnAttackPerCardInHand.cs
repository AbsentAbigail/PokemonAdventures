using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Scriptables.ScriptableAmounts;
using PokemonMod.StatusEffectImplementations;
using PokemonMod.Variables;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhileActiveReduceOwnAttackPerCardInHand : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectAcrobatics>(Name)
            .WithText("Has <-{a}><keyword=attack> for each <Item> in your hand")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectAcrobatics>(status =>
            {
                status.effectToApply = Mod.GetStatus("Ongoing Reduce Attack");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.scriptableAmount = new Script<ScriptableCardTypeInHand>(script => script.type = "Item");
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}