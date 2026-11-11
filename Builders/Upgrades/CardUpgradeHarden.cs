using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeHarden : IUpgradeBuilder
{
    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Mod.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Mod.GetCharmSprite("Harden"))
            .WithTitle("TM - Harden")
            .WithText("Gain <2><keyword=shell>")
            .InCharmPool()
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.effects = [Mod.SStack("Shell", 2)];
                charm.targetConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintCanBeHit>(),
                    TargetConstraintHelper.DoesTrigger(),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}