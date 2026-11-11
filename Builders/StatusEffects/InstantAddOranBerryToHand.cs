using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Items;
using PokemonMod.Builders.Interfaces;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class InstantAddOranBerryToHand : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectInstantSummonRandomFromPool>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantSummonRandomFromPool>(status =>
            {
                status.pool = [Mod.GetCard(OranBerry.Name)];
                status.targetSummon = Mod.GetStatusOf<StatusEffectSummon>(SummonDummy.Name);
                status.canSummonMultiple = true;
                status.summonPosition = StatusEffectInstantSummon.Position.Hand;
            });
    }
}