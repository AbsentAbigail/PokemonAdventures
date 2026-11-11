using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.LeaderPokemon;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Leaders;

[UsedImplicitly]
public class RangerKate : ITrainerBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Ranger Kate")
            .SetStats(7, null, 3)
            .SetSprites(
                Mod.GetSprite("Kate"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(OnCardPlayedAddRandomBerryToHand.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public ITrainerBuilder.MenuTrainerModifier MenuTrainerModifiers => new()
    {
        Partners = [Munchlax.Name],
        MenuSpriteName = "KateAndMunchlax",
        MenuTitle = "Kate and Munchlax",
    };
}