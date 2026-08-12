using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.FieldEffects;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class SummonSun : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return Mod.StatusCopy("Summon Beepop", Name)
            .WithText($"Summon {Mod.CardTag(HarshSunlight.Name)}")
            .SubscribeToAfterAllBuildEvent<StatusEffectSummon>(status =>
            {
                status.summonCard = Mod.GetCard(HarshSunlight.Name);
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}