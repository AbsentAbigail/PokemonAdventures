using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class InstantSummonDummyToHand : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return Mod.StatusCopy("Instant Summon Junk In Hand", Name)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantSummon>(status =>
                status.targetSummon = Mod.GetStatusOf<StatusEffectSummon>(SummonDummy.Name));
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}