using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Hoppip : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Hoppip",
                idleAnim: "FloatAnimationProfile")
            .SetStats(4, 5, 3)
            .SetSprites(
                Mod.GetSprite("Hoppip"),
                Mod.GetBackgroundSprite(BackgroundSprites.Sky))
            .DropsBling(4)
            .EvolvesInto(Skiploom.Name)
            .InUnitPool()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Grass.Name),
                    Mod.SStack(Types.Flying.Name),
                    Mod.SStack(WhileActiveReduceOwnAttackPerCardInHand.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}