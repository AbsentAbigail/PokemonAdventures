using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeLick : IUpgradeBuilder
{
    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Mod.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Mod.GetCharmSprite("Lick"))
            .WithTitle("TM - Lick")
            .WithText($"Apply {Mod.KeywordTag(Keywords.Paralysis.Name)} equal target's debuffs")
            .InCharmPool()
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.attackEffects =
                [
                    Mod.SStack(InstantApplyParalysisEqualToDebuffs.Name),
                ];
                charm.targetConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintDoesDamage>(),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}