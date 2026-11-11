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
public class Pichu : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Pichu")
            .SetStats(3, 1, 3)
            .SetSprites(
                Mod.GetSprite("Pichu"),
                Mod.GetBackgroundSprite(BackgroundSprites.Suburban))
            .DropsBling(4)
            .EvolvesInto(Pikachu.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack(Paralysis.Name),
                ];
                card.startWithEffects =
                [
                    Mod.SStack(Types.Electric.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}