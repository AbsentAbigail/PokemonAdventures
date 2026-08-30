using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeFairyWind : IUpgradeBuilder
{
    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Mod.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Mod.GetCharmSprite("FairyWind"))
            .WithTitle("TM - Fairy Wind")
            .WithText($"Deal additional damage equal to <sprite name={Types.Fairy.Keyword()}> in play")
            .InCharmPool()
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.effects = [Mod.SStack(DealAdditionalDamageEqualToFairyTypes.Name)];
                charm.targetConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintDoesDamage>(),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}