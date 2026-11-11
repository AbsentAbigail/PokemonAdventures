using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.FieldEffects;

[UsedImplicitly]
public class Sandstorm : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Sandstorm")
            .SetStats(5, 1, 2)
            .WithCardType("Summoned")
            .SetSprites(
                Mod.GetSprite("Sandstorm"),
                Mod.GetBackgroundSprite(BackgroundSprites.Sandstorm))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(HitAllPokemonExceptRockSteelGround.Name),
                    Mod.SStack(Intensity.Name),
                ];
                card.traits =
                [
                    Mod.TStack("Summoned"),
                ];
                card.charmSlots = 0;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}