using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Evolutions;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Builders.Traits;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Cards.LeaderPokemon;

[UsedImplicitly]
public class Chikorita : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Chikorita")
            .SetStats(4, 1, 3)
            .SetSprites(
                Mod.GetSprite("Chikorita"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(4)
            .EvolvesInto(Bayleaf.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.AddToPets();
                card.startWithEffects =
                [
                    Mod.SStack(Types.Grass.Name),
                    Mod.SStack(OnCardPlayedSunnyDoubleOwnAttack.Name),
                ];
                card.traits =
                [
                    Mod.TStack(LeafGuard.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}