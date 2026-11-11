using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeSupersonic : IUpgradeBuilder
{
    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Mod.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Mod.GetCharmSprite("Supersonic"))
            .WithTitle("TM - Supersonic")
            .WithText("""
                      Reduce <keyword=health> by half
                      Apply <1><keyword=haze>
                      """)
            .InCharmPool()
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.attackEffects =
                [
                    Mod.SStack("Haze"),
                ];
                charm.scripts = [
                    new Script<CardScriptMultiplyHealth>("Multiply Health by 0.5", script =>
                    {
                        script.multiply = 0.5f;
                        script.roundUp = true;
                    }),
                ];
                charm.becomesTargetedCard = true;
                charm.targetConstraints =
                [
                    TargetConstraintHelper.ApplyCharmConstraint(),
                    TargetConstraintHelper.HealthMoreThan(0),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}