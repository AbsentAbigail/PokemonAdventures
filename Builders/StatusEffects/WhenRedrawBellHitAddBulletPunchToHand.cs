using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Items;
using PokemonMod.Builders.Interfaces;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenRedrawBellHitAddBulletPunchToHand : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXAfterRedrawHit>(Name)
            .WithText($"When <Redraw Bell> is hit, add a {Mod.CardTag(BulletPunch.Name)} to hand")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXAfterRedrawHit>(status =>
            {
                status.effectToApply = Mod.GetStatus(InstantAddBulletPunchToHand.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.queue = true;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}