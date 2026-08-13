using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Evolutions;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Magnetite : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Magnetite",
                idleAnim: "FloatAnimationProfile")
            .SetStats(5, 0, 3)
            .SetSprites(
                Mod.GetSprite("Magnetite"),
                Mod.GetBackgroundSprite(BackgroundSprites.Surge))
            .DropsBling(4)
            .EvolvesInto(Magneton.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack("Weakness"),
                ];
                card.startWithEffects =
                [
                    Mod.SStack(Types.Electric.Name),
                    Mod.SStack(Types.Steel.Name),
                ];
                card.traits =
                [
                    Mod.TStack("Longshot"),
                    Mod.TStack("Pull"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}