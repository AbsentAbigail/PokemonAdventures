using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Ponyta : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Ponyta")
            .WithCardType("Enemy")
            .SetStats(4, 1, 2)
            .SetSprites(
                Mod.GetSprite("Ponyta"),
                Mod.GetBackgroundSprite(BackgroundSprites.Volcanic))
            .DropsBling(5)
            .EvolvesInto(Rapidash.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Fire.Name),
                    Mod.SStack("When Hit Apply Spice To Self", 3),
                    Mod.SStack(WhenDestroyedApplyBurnToEnemiesInRow.Name),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}