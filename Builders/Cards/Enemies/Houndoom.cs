using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Houndoom : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Houndoom")
            .WithCardType("Enemy")
            .SetStats(7, 5, 3)
            .SetSprites(
                Mod.GetSprite("Houndoom"),
                Mod.GetBackgroundSprite(BackgroundSprites.Graveyard))
            .DropsBling(8)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Dark.Name),
                    Mod.SStack(Types.Fire.Name),
                    Mod.SStack(WhenCardDestroyedApplyBurnToEnemiesInRow.Name),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}