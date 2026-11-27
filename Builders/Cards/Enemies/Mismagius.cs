using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Mismagius : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Mismagius")
            .WithCardType("Enemy")
            .SetStats(7, 1, 4)
            .SetSprites(
                Mod.GetSprite("Mismagius"),
                Mod.GetBackgroundSprite(BackgroundSprites.Graveyard))
            .DropsBling(6)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack("Demonize", 3),
                    Mod.SStack("Overload", 3),
                ];
                card.startWithEffects =
                [
                    Mod.SStack(Types.Ghost.Name),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}