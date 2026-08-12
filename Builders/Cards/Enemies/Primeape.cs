using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Primeape : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Primeape")
            .WithCardType("Enemy")
            .SetStats(8, 2, 4)
            .SetSprites(
                Mod.GetSprite("Primeape"),
                Mod.GetBackgroundSprite(BackgroundSprites.Suburban))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Fighting.Name),
                    Mod.SStack("MultiHit", 3),
                    Mod.SStack("When Hit Gain Attack To Self (No Ping)"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}