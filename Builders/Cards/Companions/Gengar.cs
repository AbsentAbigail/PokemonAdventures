using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Companions;

[UsedImplicitly]
public class Gengar : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Gengar",
                idleAnim: "FloatAnimationProfile")
            .SetStats(6, 4, 4)
            .SetSprites(
                Mod.GetSprite("Gengar"),
                Mod.GetBackgroundSprite(BackgroundSprites.Graveyard))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Ghost.Name),
                    Mod.SStack(Types.Poison.Name),
                    Mod.SStack(WhenUnitIsKilledApplySleepToRandomEnemy.Name),
                    Mod.SStack(WhenAnythingSleepdDealDamageEqualToAttack.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}