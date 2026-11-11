using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Items;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhileActiveBulletPunchHaveEqualDamage : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectWhileActiveX>(Name)
            .WithText($"While active, add equal <keyword=attack> to all {Mod.CardTag(BulletPunch.Name)} in your hand")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectWhileActiveX>(status =>
            {
                status.effectToApply = Mod.GetStatus("Ongoing Increase Attack");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Hand;
                status.applyConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintIsSpecificCard>("Is Bullet Punch", 
                        script => script.allowedCards = [Mod.GetCard(BulletPunch.Name)]),
                ];
                status.scriptableAmount = new Script<ScriptableCurrentAttack>();
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}