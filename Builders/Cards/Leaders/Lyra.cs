using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.LeaderPokemon;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Cards.Leaders;

[UsedImplicitly]
public class Lyra : ITrainerBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Lyra")
            .SetStats(10, null, 8)
            .SetSprites(
                Mod.GetSprite("Lyra"),
                Mod.GetBackgroundSprite(BackgroundSprites.Suburban))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(SummonSun.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public ITrainerBuilder.MenuTrainerModifier MenuTrainerModifiers => new()
    {
        Partners = [Chikorita.Name],
        MenuSpriteName = "LyraAndChikorita",
        MenuTitle = "Lyra and Chikorita",
    };
}