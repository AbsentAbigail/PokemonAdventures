using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeLeer : IUpgradeBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Mod.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Mod.GetSprite("CardUpgradeLeer"))
            .WithTitle("TM - Leer")
            .WithText("""
                      <-2><keyword=attack>
                      Apply <2><keyword=demonize>
                      """)
            .InCharmPool()
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.becomesTargetedCard = true;
                charm.damage = -2;
                charm.attackEffects = [Mod.SStack("Demonize", 2)];
                charm.targetConstraints =
                [
                    TargetConstraintHelper.ApplyCharmConstraint(),
                    TargetConstraintHelper.AttackMoreThan(1),
                ];
            });
    }
}