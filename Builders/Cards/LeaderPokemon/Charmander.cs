using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Evolutions;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.LeaderPokemon;

[UsedImplicitly]
public class Charmander : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Charmander")
            .SetStats(3, 3, 4)
            .SetSprites(
                Mod.GetSprite("Charmander"),
                Mod.GetBackgroundSprite(BackgroundSprites.Suburban))
            .DropsBling(4)
            .EvolvesInto(Charmeleon.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.AddToPets();
                card.attackEffects =
                [
                    Mod.SStack(Burn.Name),
                ];
                card.startWithEffects =
                [
                    Mod.SStack(Types.Fire.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}