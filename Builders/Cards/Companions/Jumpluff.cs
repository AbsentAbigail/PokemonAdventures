using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Jumpluff : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Jumpluff",
                idleAnim: "FloatAnimationProfile")
            .SetStats(8, 15, 3)
            .SetSprites(
                Mod.GetSprite("Jumpluff"),
                Mod.GetBackgroundSprite(BackgroundSprites.Sky))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Grass.Name),
                    Mod.SStack(Types.Flying.Name),
                    Mod.SStack(WhileActiveReduceOwnAttackPerCardInHand.Name, 3),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}