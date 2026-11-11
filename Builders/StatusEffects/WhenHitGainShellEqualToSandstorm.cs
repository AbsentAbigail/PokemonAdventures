using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Enemies;
using PokemonMod.Builders.Cards.FieldEffects;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Helpers;
using PokemonMod.Scriptables.ScriptableAmounts;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenHitGainShellEqualToSandstorm : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenHit>(Name)
            .WithText($"When hit, gain <keyword=shell> equal to {Mod.CardTag(Sandstorm.Name)} intensity")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenHit>(status =>
            {
                status.effectToApply = Mod.GetStatus("Shell");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.scriptableAmount = new Script<ScriptableWeatherIntensity>(
                    "Sandstorm Intensity",
                    weather =>
                    {
                        weather.intensityEffect = Mod.GetStatus(Intensity.Name);
                        weather.weatherCard = Mod.GetCard(Sandstorm.Name);
                    });
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}