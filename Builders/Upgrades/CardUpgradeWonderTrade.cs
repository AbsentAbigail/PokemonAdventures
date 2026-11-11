using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Scriptables.CardScripts;

namespace PokemonMod.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeWonderTrade : IUpgradeBuilder
{
    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Mod.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Mod.GetCharmSprite("WonderTrade"))
            .WithTitle("Wonder Trade")
            .WithText($"When equipped to a <Companion>, trade it for a random <Legendary> or <Baby> Pokemon")
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.targetConstraints =
                [
                    TargetConstraintHelper.IsCardType(["Friendly"]),
                ];
                charm.scripts =
                [
                    new Script<WonderTradeScript>(),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}