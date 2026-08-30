using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Items;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenDestroyedAddSalacBerryToHand : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenDestroyed>(Name)
            .WithText($"When destroyed, add <{{a}}> {Mod.CardTag(SalacBerry.Name)} to hand")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDestroyed>(status =>
            {
                status.effectToApply = Mod.GetStatus(InstantAddSalacBerryToHand.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.targetMustBeAlive = false;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}