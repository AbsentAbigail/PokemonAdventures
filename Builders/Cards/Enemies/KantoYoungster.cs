using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class KantoYoungster : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Kanto Youngster")
            .WithCardType("Miniboss")
            .SetStats(6, null, 3)
            .SetSprites(
                Mod.GetSprite("KantoYoungster"),
                Mod.GetBackgroundSprite(BackgroundSprites.Battlefield))
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(OnCardPlayedIncreaseAttackOfNormalAllies.Name),
                    Mod.SStack(OnCardPlayedApplyFrostToEveryoneInRow.Name),
                    Mod.SStack(WhenDestroyedBossReward.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}