using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class SummonSubstitute : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return Mod.StatusCopy("Summon Plep", Name)
            .SubscribeToAfterAllBuildEvent<StatusEffectSummon>(status =>
            {
                status.summonCard = Mod.GetCard(Cards.Clunkers.Substitute.Name);
                status.gainTrait = null;
                status.setCardType = null;
                status.effectPrefabRef = Mod.GetStatusOf<StatusEffectSummon>("Summon Junk").effectPrefabRef;
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}