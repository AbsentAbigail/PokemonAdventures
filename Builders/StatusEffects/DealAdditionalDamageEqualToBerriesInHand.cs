using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Helpers;
using PokemonMod.Scriptables.ScriptableAmounts;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class DealAdditionalDamageEqualToBerriesInHand : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectDealAdditionalDamage>(Name)
            .WithText("Deal additional damage equal to <Berries> in hand")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectDealAdditionalDamage>(status =>
            {
                status.hiddenKeywords = [Mod.GetKeyword(Berry.Name)];
                status.scriptableAmount = new Script<ScriptableCardsWithTraitInHand>(script =>
                {
                    script.traitData = Mod.GetTrait(Traits.Berry.Name);
                });
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}