using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeMudShot : IUpgradeBuilder
{
    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Mod.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Mod.GetCharmSprite("MudShot"))
            .WithTitle("TM53 - Mud Shot")
            .WithText("Increase target's <keyword=counter> by <2>")
            .InCharmPool()
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.attackEffects =
                [
                    Mod.SStack("Increase Max Counter", 2),
                ];
                charm.becomesTargetedCard = true;
                charm.targetConstraints =
                [
                    TargetConstraintHelper.ApplyCharmConstraint(),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}