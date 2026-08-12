using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Persian : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Persian")
            .WithCardType("Enemy")
            .SetStats(9, 2, 3)
            .SetSprites(
                Mod.GetSprite("Persian"),
                Mod.GetBackgroundSprite(BackgroundSprites.Suburban))
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Normal.Name),
                    Mod.SStack("MultiHit"),
                    Mod.SStack(OnHitGainEqualBling.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}