using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Golem : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Golem")
            .WithCardType("Enemy")
            .SetStats(5, 3, 5)
            .SetSprites(
                Mod.GetSprite("Golem"),
                Mod.GetBackgroundSprite(BackgroundSprites.Cave))
            .DropsBling(5)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Rock.Name),
                    Mod.SStack(Types.Ground.Name),
                    Mod.SStack("Shell", 3),
                    Mod.SStack("On Card Played Apply Attack To Self", 2),
                    Mod.SStack(WhenSandstormPlayedReduceCounter.Name),
                    Mod.SStack(CastSandstorm.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}