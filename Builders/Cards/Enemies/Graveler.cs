using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Graveler : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Graveler")
            .WithCardType("Enemy")
            .SetStats(4, 2, 5)
            .SetSprites(
                Mod.GetSprite("Graveler"),
                Mod.GetBackgroundSprite(BackgroundSprites.Cave))
            .DropsBling(5)
            .EvolvesInto(Golem.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Rock.Name),
                    Mod.SStack(Types.Ground.Name),
                    Mod.SStack("Shell", 2),
                    Mod.SStack("On Turn Apply Shell To Self"),
                    Mod.SStack("On Card Played Apply Attack To Self"),
                    Mod.SStack(WhenDeployedCastSandstorm.Name),
                    Mod.SStack(WhenSandstormPlayedReduceCounter.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}