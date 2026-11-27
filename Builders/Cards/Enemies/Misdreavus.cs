using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Misdreavus : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Misdreavus")
            .WithCardType("Enemy")
            .SetStats(6, 1, 5)
            .SetSprites(
                Mod.GetSprite("Misdreavus"),
                Mod.GetBackgroundSprite(BackgroundSprites.Graveyard))
            .DropsBling(4)
            .EvolvesInto(Mismagius.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack("Demonize"),
                    Mod.SStack("Overload", 2),
                ];
                card.startWithEffects =
                [
                    Mod.SStack(Types.Ghost.Name),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}