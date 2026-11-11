using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class TrainerBugCatcher : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Bug Catcher")
            .WithCardType("Miniboss")
            .SetStats(10, null, 5)
            .DropsBling(13)
            .SetSprites(
                Mod.GetSprite("BugCatcher"),
                Mod.GetBackgroundSprite(BackgroundSprites.Battlefield))
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(OnCardPlayedAddOranBerryToHand.Name, 2),
                    Mod.SStack(WhenBerryPlayedTriggerAllies.Name),
                    Mod.SStack(BossRewardBugCatcher.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}