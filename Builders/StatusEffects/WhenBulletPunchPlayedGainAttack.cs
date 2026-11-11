using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Items;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhenBulletPunchPlayedGainAttack : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXWhenCertainCardPlayed>(Name)
            .WithText($"When {Mod.CardTag(BulletPunch.Name)} is played, gain <+{{a}}><keyword=attack>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenCertainCardPlayed>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.effectToApply = Mod.GetStatus("Increase Attack");
                status.cardConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintIsSpecificCard>("Is Bullet Punch", 
                        script => script.allowedCards = [Mod.GetCard(BulletPunch.Name)]),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}