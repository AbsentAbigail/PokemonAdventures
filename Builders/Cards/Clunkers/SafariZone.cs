using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders.Cards.Clunkers;

[UsedImplicitly]
public class SafariZone : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Mod.Instance)
            .CreateUnit(Name, "Safari Zone")
            .WithCardType("Clunker")
            .SetStats(null, null, 4)
            .SetSprites(
                Mod.GetSprite("SafariZone"),
                Mod.GetBackgroundSprite(BackgroundSprites.Bog))
            .DropsBling(35)
            .InItemPool()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Mod.SStack("Scrap", 3),
                    Mod.SStack(OnCardPlayedHealEnemies.Name, 4),
                    Mod.SStack(OnCardPlayedAddPokeballToHand.Name),
                    Mod.SStack(WhileActivePokeballDealMoreDamage.Name, 3),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}