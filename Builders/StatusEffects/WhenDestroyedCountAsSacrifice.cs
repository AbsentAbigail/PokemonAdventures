using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Items;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenDestroyedCountAsSacrifice : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenDestroyed>(Name)
            .WithText("When destroyed, it counts as a sacrifice")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDestroyed>(status =>
            {
                status.effectToApply = Mod.GetStatus(InstantActivateSacrifice.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.targetMustBeAlive = false;
                
                status.eventPriority = int.MaxValue;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}