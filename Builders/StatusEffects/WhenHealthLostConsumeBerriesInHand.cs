using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using Berry = PokemonMod.Builders.Keywords.Berry;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenHealthLostConsumeBerriesInHand : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenHealthLost>(Name)
            .WithText("When <keyword=health> lost, consume all <Berries> in hand")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenHealthLost>(status =>
            {
                status.effectToApply = Mod.GetStatus(InstantConsume.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Hand;
                status.applyConstraints =
                [
                    TargetConstraintHelper.HasTrait(Traits.Berry.Name),
                ];
                status.hiddenKeywords =
                [
                    Mod.GetKeyword(Berry.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}