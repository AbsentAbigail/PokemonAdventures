using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.Traits;
using PokemonMod.Builders.Upgrades;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class TrainerPunkCouple : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Punk Couple")
            .WithCardType("Miniboss")
            .SetStats(30, null, 3)
            .DropsBling(15)
            .SetSprites(
                Mod.GetSprite("PunkCouple"),
                Mod.GetBackgroundSprite(BackgroundSprites.Battlefield))
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.traits =
                [
                    Mod.TStack(TagTeamPunkCouple.Name),
                ];
            })
            .WithBossRewards([
                Mod.PrefixGuid(CardUpgradeWillOWisp.Name),
                Mod.PrefixGuid(CardUpgradeAssurance.Name),
                Mod.PrefixGuid(CardUpgradeScreech.Name),
            ]);
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}