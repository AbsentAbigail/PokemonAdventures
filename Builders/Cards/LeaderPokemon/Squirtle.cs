using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Evolutions;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Cards.LeaderPokemon;

[UsedImplicitly]
public class Squirtle : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Squirtle")
            .SetStats(4, 1, 4)
            .SetSprites(
                Mod.GetSprite("Squirtle"),
                Mod.GetBackgroundSprite(BackgroundSprites.Sky))
            .DropsBling(4)
            .EvolvesInto(Wartortle.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.AddToPets();
                card.startWithEffects =
                [
                    Mod.SStack(Types.Water.Name),
                    Mod.SStack(WhenDeployedGainShell.Name),
                    Mod.SStack(OnCardPlayedDrawEqualToShell.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}