using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeSludge : IUpgradeBuilder
{
    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Mod.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Mod.GetCharmSprite("Sludge"))
            .WithTitle("TM - Sludge")
            .WithText("""
                      <keyword=longshot>
                      Apply <2><keyword=shroom>
                      """)
            .InCharmPool()
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.attackEffects =
                [
                    Mod.SStack("Shroom", 2),
                ];
                charm.giveTraits =
                [
                    Mod.TStack("Longshot"),
                ];
                charm.becomesTargetedCard = true;
                charm.targetConstraints =
                [
                    TargetConstraintHelper.ApplyCharmConstraint(),
                    TargetConstraintHelper.HasTrait("Barrage", not: true),
                    TargetConstraintHelper.HasTrait("Aimless", not: true),
                    TargetConstraintHelper.HasTrait("Longshot", not: true),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}