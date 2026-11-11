using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class TrainerBiker : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Biker")
            .WithCardType("Miniboss")
            .SetStats(25, null, 5)
            .DropsBling(15)
            .SetSprites(
                Mod.GetSprite("Biker"),
                Mod.GetBackgroundSprite(BackgroundSprites.Battlefield))
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(OnCardPlayedIncreaseAttackOfPoisonAlliesAllies.Name),
                    Mod.SStack(WhileActiveShroomRetainsAtLeast.Name),
                    Mod.SStack(BossRewardBiker.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}