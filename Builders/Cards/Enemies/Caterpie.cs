using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Caterpie : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Caterpie")
            .WithCardType("Enemy")
            .SetStats(4, 1, 4)
            .SetSprites(
                Mod.GetSprite("Caterpie"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(2)
            .EvolvesInto(Metapod.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Bug.Name),
                    Mod.SStack(OnCardPlayedConsumeBerriesInHand.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}