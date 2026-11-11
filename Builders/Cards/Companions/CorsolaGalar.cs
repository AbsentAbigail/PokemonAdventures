using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class CorsolaGalar : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Corsola")
            .SetStats(3, 1, 4)
            .SetSprites(
                Mod.GetSprite("CorsolaGalar"),
                Mod.GetBackgroundSprite(BackgroundSprites.Graveyard))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Ghost.Name),
                    Mod.SStack("On Card Played Add Soulbound To RandomEnemy"),
                ];
                card.traits =
                [
                    Mod.TStack("Soulbound"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}