using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Cards.Evolutions;

[UsedImplicitly]
public class Magnezone : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Magnezone",
                idleAnim: "FloatAnimationProfile")
            .SetStats(11, 0, 3)
            .SetSprites(
                Mod.GetSprite("Magnezone"),
                Mod.GetBackgroundSprite(BackgroundSprites.Surge))
            .DropsBling(4)
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
                    Mod.TStack("Barrage"),
                    Mod.TStack("Pull"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}