using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;
using Berry = PokemonMod.Builders.Keywords.Berry;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenBerryConsumedAddCopyToDiscardPile : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectRecycleItem>(Name)
            .WithText($"When a {Mod.KeywordTag(Berry.Name)} is consumed, add a copy of it to the <Discard Pile>")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectRecycleItem>(status =>
            {
                status.summonEffect = Mod.GetStatusOf<StatusEffectSummon>(SummonBerry.Name);
                status.cardConstraints =
                [
                    TargetConstraintHelper.HasTrait(Traits.Berry.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}