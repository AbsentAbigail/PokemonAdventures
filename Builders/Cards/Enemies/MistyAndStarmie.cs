using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class MistyAndStarmie : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Misty and Starmie")
            .WithCardType("Boss")
            .SetStats(9, 1, 3)
            .SetSprites(
                Mod.GetSprite("MistyAndStarmie"),
                Mod.GetBackgroundSprite(BackgroundSprites.Ocean))
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Water.Name),
                    Mod.SStack(Types.Psychic.Name),
                    Mod.SStack(WhileActiveFrenzyEqualToAllies.Name),
                    Mod.SStack(MistyAndStarmiePhase2.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}