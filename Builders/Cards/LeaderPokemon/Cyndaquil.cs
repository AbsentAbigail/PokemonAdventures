using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Evolutions;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Cards.LeaderPokemon;

[UsedImplicitly]
public class Cyndaquil : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Cyndaquil")
            .SetStats(4, 3, 5)
            .SetSprites(
                Mod.GetSprite("Cyndaquil"),
                Mod.GetBackgroundSprite(BackgroundSprites.Suburban))
            .DropsBling(4)
            .EvolvesInto(Quilava.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.AddToPets();
                card.startWithEffects =
                [
                    Mod.SStack(Types.Fire.Name),
                    Mod.SStack(WhenItemUsedApplySpiceToAllyBehind.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}