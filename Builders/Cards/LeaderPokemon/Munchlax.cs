using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Builders.Traits;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.LeaderPokemon;

[UsedImplicitly]
public class Munchlax : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Munchlax")
            .SetStats(7, 5, 3)
            .SetSprites(
                Mod.GetSprite("Munchlax"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(4)
            .EvolvesInto(Snorlax.Name)
            .MakePet()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Normal.Name),
                    Mod.SStack(Sleep.Name, 6),
                    Mod.SStack(WhenBerryPlayedOnSelfApplyFrenzy.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}