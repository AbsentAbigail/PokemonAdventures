using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Traits;
using PokemonMod.Helpers;
using PokemonMod.Scriptables.ScriptableAmounts;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class InstantApplyShroomEqualToBerries : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXInstant>(Name)
            .WithText("Apply <keyword=shroom> equal to <Berries> in hand")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXInstant>(status =>
            {
                status.effectToApply = Mod.GetStatus("Shroom");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.scriptableAmount = new Script<ScriptableCardsWithTraitInHand>(script => script.traitData = Mod.GetTrait(Berry.Name));
                status.hiddenKeywords =
                [
                    Mod.GetKeyword(Keywords.Berry.Name),
                ];
            });
    }

}