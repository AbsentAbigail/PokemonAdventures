using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class OnCardPlayedApplySnowToRandomNonIceAllyAndEnemy : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXOnCardPlayed>(Name)
            .WithText($"Apply <{{a}}><keyword=snow> to a random non-<sprite name={Types.Ice.Keyword()}> ally and enemy")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnCardPlayed>(status =>
            {
                status.effectToApply = Mod.GetStatus("Snow");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.RandomAlly | StatusEffectApplyX.ApplyToFlags.RandomEnemy;
                status.applyConstraints =
                [
                    TargetConstraintHelper.IsOfTypes([Types.Ice.Name], true),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}