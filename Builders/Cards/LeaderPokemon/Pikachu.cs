using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Builders.Traits;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.LeaderPokemon;

[UsedImplicitly]
public class Pikachu : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Pikachu")
            .SetStats(6, 1, 3)
            .SetSprites(
                Mod.GetSprite("Pikachu"),
                Mod.GetBackgroundSprite(BackgroundSprites.Suburban))
            .DropsBling(4)
            .EvolvesInto(Raichu.Name)
            .MakePet()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Electric.Name),
                    Mod.SStack(OnCardPlayedApplyParalysisToEnemiesInRow.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}