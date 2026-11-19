using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Clunkers;

[UsedImplicitly]
public class PalletTown : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Pallet Town")
            .WithCardType("Clunker")
            .SetStats(null, null, 4)
            .SetSprites(
                Mod.GetSprite("PalletTown"),
                Mod.GetBackgroundSprite(BackgroundSprites.Sky))
            .DropsBling(35)
            .InItemPool()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack("Scrap"),
                    Mod.SStack(WhenDeployedDraw.Name, 3),
                ];
                card.traits =
                [
                    Mod.TStack("Draw"),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}