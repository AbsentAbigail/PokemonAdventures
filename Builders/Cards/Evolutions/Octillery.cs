using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Evolutions;

[UsedImplicitly]
public class Octillery : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Octillery")
            .WithCardType("Enemy")
            .SetStats(3, 1, 5)
            .SetSprites(
                Mod.GetSprite("Octillery"),
                Mod.GetBackgroundSprite(BackgroundSprites.Ocean))
            .DropsBling(5)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack("Null"),
                ];
                card.startWithEffects =
                [
                    Mod.SStack(Types.Water.Name),
                    Mod.SStack("MultiHit", 4),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}