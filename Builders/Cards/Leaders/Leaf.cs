using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.LeaderPokemon;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Cards.Leaders;

[UsedImplicitly]
public class Leaf : ITrainerBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Leaf")
            .SetStats(7, null, 4)
            .SetSprites(
                Mod.GetSprite("Leaf"),
                Mod.GetBackgroundSprite(BackgroundSprites.Suburban))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(OnCardPlayedHealAlliesInRow.Name, 2),
                    Mod.SStack(WhileActiveGrassAlliesHaveFlourish.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public ITrainerBuilder.MenuTrainerModifier MenuTrainerModifiers => new()
    {
        Partners = [Bulbasaur.Name],
        MenuSpriteName = "LeafAndBulbasaur",
        MenuTitle = "Leaf and Bulbasaur",
    };
}