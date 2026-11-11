using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Geodude : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Geodude")
            .WithCardType("Enemy")
            .SetStats(3, 1, 5)
            .SetSprites(
                Mod.GetSprite("Geodude"),
                Mod.GetBackgroundSprite(BackgroundSprites.Cave))
            .DropsBling(5)
            .EvolvesInto(Graveler.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Rock.Name),
                    Mod.SStack(Types.Ground.Name),
                    Mod.SStack("Shell"),
                    Mod.SStack("On Turn Apply Shell To Self"),
                    Mod.SStack(OnCardPlayedReduceCounter.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}