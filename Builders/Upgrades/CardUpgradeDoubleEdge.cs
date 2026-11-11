using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeDoubleEdge : IUpgradeBuilder
{
    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Mod.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Mod.GetCharmSprite("DoubleEdge"))
            .WithTitle("TM10 - Double-Edge")
            .WithText("""
                      On kill, gain <+3><keyword=attack>
                      On kill, deal <1> damage to self 
                      """)
            .InCharmPool()
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.effects =
                [
                    Mod.SStack(OnKillIncreaseAttack.Name, 3),
                    Mod.SStack(OnKillTakeDamage.Name),
                ];
                charm.targetConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintDoesDamage>(),
                    TargetConstraintHelper.AttackMoreThan(0),
                    TargetConstraintHelper.General<TargetConstraintCanBeHit>(),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}