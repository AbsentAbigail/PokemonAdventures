using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Keywords;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeAssurance : IUpgradeBuilder
{
    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Mod.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Mod.GetCharmSprite("Assurance"))
            .WithTitle("TM58 - Assurance")
            .WithText("Deal <3> additional damage to damaged targets")
            .InCharmPool()
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.attackEffects = [Mod.SStack(InstantDealDamageToDamagedTarget.Name)];
                charm.targetConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintDoesDamage>(),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}