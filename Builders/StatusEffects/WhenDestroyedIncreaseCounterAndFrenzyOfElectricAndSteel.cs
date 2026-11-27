using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class WhenDestroyedIncreaseCounterAndFrenzyOfElectricAndSteel : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenDestroyed>(Name)
            .WithText(
                $"When destroyed, increase <keyword=counter> and <keyword=frenzy> of <sprite name={Types.Electric.Keyword()}> and <sprite name={Types.Steel.Keyword()}> by <{{a}}>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDestroyed>(status =>
            {
                status.effectToApply = Mod.GetStatus(InstantMultipleIncreaseCounterFrenzy.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies | StatusEffectApplyX.ApplyToFlags.Enemies;
                status.applyConstraints =
                [
                    TargetConstraintHelper.IsOfTypes([
                        Types.Electric.Name,
                        Types.Steel.Name,
                    ]),
                ];
                status.targetMustBeAlive = false;
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}