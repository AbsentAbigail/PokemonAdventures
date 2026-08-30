using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Scriptables.CardScripts;

namespace PokemonMod.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeLinkCable : IUpgradeBuilder
{
    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Mod.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Mod.GetCharmSprite("LinkCable"))
            .WithTitle("Link Cable")
            .WithText($"When equipped to a <Companion>, choose <1> of <3> units to trade for")
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.targetConstraints =
                [
                    TargetConstraintHelper.IsCardType(["Friendly"]),
                ];
                charm.scripts =
                [
                    new Script<LinkCableScript>(),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}