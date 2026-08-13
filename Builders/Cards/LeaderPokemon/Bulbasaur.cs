using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Evolutions;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;
using LeechSeed = PokemonMod.Builders.Traits.LeechSeed;

namespace PokemonMod.Builders.Cards.LeaderPokemon;

[UsedImplicitly]
public class Bulbasaur : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Bulbasaur")
            .SetStats(5, 0, 4)
            .SetSprites(
                Mod.GetSprite("Bulbasaur"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(4)
            .EvolvesInto(Ivysaur.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.AddToPets();
                card.attackEffects =
                [
                    Mod.SStack(Constricted.Name),
                ];
                card.startWithEffects =
                [
                    Mod.SStack(Types.Grass.Name),
                    Mod.SStack(Types.Poison.Name),
                ];
                card.traits =
                [
                    Mod.TStack(LeechSeed.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}