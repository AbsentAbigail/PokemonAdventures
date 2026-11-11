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
public class Yellow : ITrainerBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Yellow")
            .SetStats(4, null, 3)
            .SetSprites(
                Mod.GetSprite("Yellow"),
                Mod.GetBackgroundSprite(BackgroundSprites.Suburban))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(OnCardPlayedCleanseElectricAllies.Name),
                    Mod.SStack(WhenAnyoneParalysedDoubleAmount.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public ITrainerBuilder.MenuTrainerModifier MenuTrainerModifiers => new()
    {
        Partners = [Pikachu.Name],
        MenuSpriteName = "YellowAndPikachu",
        MenuTitle = "Yellow and Pikachu",
    };
}