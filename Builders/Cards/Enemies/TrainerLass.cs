using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class TrainerLass : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Lass")
            .WithCardType("Miniboss")
            .SetStats(13, null, 5)
            .DropsBling(14)
            .SetSprites(
                Mod.GetSprite("Lass"),
                Mod.GetBackgroundSprite(BackgroundSprites.Battlefield))
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(OnCardPlayedHealFlyingAllies.Name, 3),
                    Mod.SStack(WhenPsychicAllyHitApplySleepToEveryone.Name),
                    Mod.SStack(BossRewardLass.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}