using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.Cards.FieldEffects;

[UsedImplicitly]
public class HarshSunlight : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Harsh Sunlight")
            .SetStats(4, null, 2)
            .WithCardType("Summoned")
            .SetSprites(
                Mod.GetSprite("HarshSunlight"),
                Mod.GetBackgroundSprite(BackgroundSprites.Sky))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(WhileActiveFireIncreaseAttack.Name),
                    Mod.SStack(WhileActiveWaterDecreaseAttack.Name),
                    Mod.SStack(OnCardPlayedHealGrass.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}