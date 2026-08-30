using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeMegaDrain : IUpgradeBuilder
{
    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Mod.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Mod.GetCharmSprite("MegaDrain"))
            .WithTitle("TM21 - Mega Drain")
            .WithText("Restore <keyword=health> equal to damage dealt")
            .InCharmPool()
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.effects = [Mod.SStack(OnHitRestoreEqualHealth.Name)];
                charm.targetConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintDoesDamage>(),
                    TargetConstraintHelper.HealthMoreThan(0),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}