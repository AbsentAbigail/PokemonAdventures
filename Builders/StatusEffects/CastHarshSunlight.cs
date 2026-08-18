using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.FieldEffects;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class CastHarshSunlight : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectCastWeather>(Name)
            .WithText($"{Mod.KeywordTag(Cast.Name)} <{{a}}> {Mod.CardTag(HarshSunlight.Name)}")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectCastWeather>(status =>
            {
                status.summonCard = Mod.GetCard(HarshSunlight.Name);
                status.effectPrefabRef = Mod.GetStatusOf<StatusEffectSummon>("Summon Beepop").effectPrefabRef;
                status.intensityEffect = Mod.GetStatus(Intensity.Name);
                status.increaseHealthEffect = Mod.GetStatus("Increase Max Health");
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}