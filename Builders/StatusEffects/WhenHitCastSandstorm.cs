using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Enemies;
using PokemonMod.Builders.Cards.FieldEffects;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenHitCastSandstorm : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenHit>(Name)
            .WithText($"When hit, {Mod.KeywordTag(Cast.Name)} <{{a}}> {Mod.CardTag(Sandstorm.Name)}")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenHit>(status =>
            {
                status.effectToApply = Mod.GetStatus(InstantCastSandstorm.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}