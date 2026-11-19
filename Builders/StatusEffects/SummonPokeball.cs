using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Items;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class SummonPokeball : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return Mod.StatusCopy("Summon Junk", Name)
            .SubscribeToAfterAllBuildEvent<StatusEffectSummon>(status =>
            {
                status.summonCard = Mod.GetCard(Cards.Items.Pokeball.Name);
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}