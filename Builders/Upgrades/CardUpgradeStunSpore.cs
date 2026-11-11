using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeStunSpore : IUpgradeBuilder
{
    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Mod.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Mod.GetCharmSprite("StunSpore"))
            .WithTitle("TM - Stun Spore")
            .WithText($"Apply <2>{Mod.KeywordTag(Paralysis.Name)}")
            .InCharmPool()
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.becomesTargetedCard = true;
                charm.attackEffects = [Mod.SStack(StatusEffects.Paralysis.Name, 2)];
                charm.targetConstraints =
                [
                    TargetConstraintHelper.ApplyCharmConstraint(),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}