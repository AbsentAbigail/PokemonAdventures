using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeRockBlast : IUpgradeBuilder
{
    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Mod.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Mod.GetCharmSprite("RockBlast"))
            .WithTitle("TM54 - Rock Blast")
            .WithText("""
                      <+2><keyword=counter>
                      Has bonus <keyword=frenzy> equal to <keyword=shell>
                      """)
            .InCharmPool()
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.counter = 2;
                charm.effects =
                [
                    Mod.SStack(WhileActiveFrenzyEqualToShell.Name),
                ];
                charm.targetConstraints =
                [
                    TargetConstraintHelper.ApplyCharmConstraint(),
                    TargetConstraintHelper.MaxCounterMoreThan(2),
                    TargetConstraintHelper.General<TargetConstraintCanBeHit>(),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}