using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Evolutions;

[UsedImplicitly]
public class Magneton : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Magneton",
                idleAnim: "FloatAnimationProfile")
            .SetStats(8, 0, 3)
            .SetSprites(
                Mod.GetSprite("Magneton"),
                Mod.GetBackgroundSprite(BackgroundSprites.Surge))
            .DropsBling(4)
            .EvolvesInto(Magnezone.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Mod.SStack("Weakness", 3),
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