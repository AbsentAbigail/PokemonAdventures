using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeBugBite : IUpgradeBuilder
{
    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Mod.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Mod.GetCharmSprite("BugBite"))
            .WithTitle("TM182 - Bug Bite")
            .WithText($"When eating a {Mod.KeywordTag(Berry.Name)}, gain <+2><keyword=health>")
            .InCharmPool()
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.effects = [Mod.SStack(WhenBerryPlayedOnSelfGainHealth.Name, 2)];
                charm.targetConstraints =
                [
                    TargetConstraintHelper.HealthMoreThan(0),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}