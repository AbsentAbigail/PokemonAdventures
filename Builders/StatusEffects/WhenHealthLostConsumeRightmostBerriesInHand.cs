using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using Berry = PokemonMod.Builders.Keywords.Berry;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class WhenHealthLostConsumeRightmostBerriesInHand : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenHealthLost>(Name)
            .WithText("When <keyword=health> lost, consume the <{a}> rightmost <Berries> in hand")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenHealthLost>(status =>
            {
                status.effectToApply = Mod.GetStatus(InstantConsumeRightmostBerries.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.hiddenKeywords =
                [
                    Mod.GetKeyword(Berry.Name),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}