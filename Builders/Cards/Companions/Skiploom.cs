using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Skiploom : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Skiploom",
                idleAnim: "FloatAnimationProfile")
            .SetStats(6, 10, 3)
            .SetSprites(
                Mod.GetSprite("Skiploom"),
                Mod.GetBackgroundSprite(BackgroundSprites.Sky))
            .DropsBling(4)
            .EvolvesInto(Jumpluff.Name)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Grass.Name),
                    Mod.SStack(Types.Flying.Name),
                    Mod.SStack(WhileActiveReduceOwnAttackPerCardInHand.Name, 2),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}