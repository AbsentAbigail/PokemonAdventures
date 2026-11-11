using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Gastly : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Gastly",
                idleAnim: "FloatAnimationProfile")
            .SetStats(2, 1, 4)
            .SetSprites(
                Mod.GetSprite("Gastly"),
                Mod.GetBackgroundSprite(BackgroundSprites.Graveyard))
            .DropsBling(4)
            .EvolvesInto(Haunter.Name)
            .InUnitPool()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Ghost.Name),
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack(WhenAnythingSleepdDealDamage.Name, 2),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}