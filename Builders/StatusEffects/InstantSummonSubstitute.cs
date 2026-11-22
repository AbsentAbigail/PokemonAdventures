using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class InstantSummonSubstitute : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectInstantSummonSubstitute>(Name)
            .WithText($"""
                       Replace a Pokemon with a {Mod.CardTag(Cards.Clunkers.Substitute.Name)} that copies its <keyword=attack>, <keyword=counter>, and effects
                       When it is destroyed, the Pokemon returns to the field
                       """)
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantSummonSubstitute>(status =>
            {
                status.targetSummon = Mod.GetStatusOf<StatusEffectSummon>(SummonSubstitute.Name);
                status.substituteEffect = Mod.GetStatus(Substitute.Name);
                status.targetConstraints =
                [
                    TargetConstraintHelper.IsCardType(["Friendly", "Enemy"]),
                    TargetConstraintHelper.IsPokemon(),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}