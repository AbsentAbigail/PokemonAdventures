using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Enemies;

[UsedImplicitly]
public class Exeggutor : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Exeggutor")
            .WithCardType("Enemy")
            .SetStats(8, 2, 4)
            .SetSprites(
                Mod.GetSprite("Exeggutor"),
                Mod.GetBackgroundSprite(BackgroundSprites.Garden))
            .DropsBling(8)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack(Types.Grass.Name),
                    Mod.SStack(Types.Psychic.Name),
                    Mod.SStack(DealAdditionalDamageEqualToBerriesInHand.Name),
                    Mod.SStack(WhenBerryConsumedAddCopyToDiscardPile.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}