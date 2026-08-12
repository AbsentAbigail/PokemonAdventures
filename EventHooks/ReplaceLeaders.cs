using System.Linq;
using PokemonMod.Helpers;

namespace PokemonMod.EventHooks;

public static class ReplaceLeaders
{
    public static void PreCampaignPopulate()
    {
        var deck = References.PlayerData.inventory.deck;
        foreach (var cardInDeck in deck.ToArray())
        {
            var customData = cardInDeck.GetCustomDataOrNull("pokemon.replacePreRun");
            if (customData is not SaveCollection<string> replaceWith)
            {
                continue;
            }
            ReplaceWithCards(cardInDeck, replaceWith);
        }
    }

    private static void ReplaceWithCards(CardData card, SaveCollection<string> replaceWith)
    {
        var deck = References.PlayerData.inventory.deck;
        
        var index = 0;
        foreach (var newCardName in replaceWith.collection)
        {
            var newCard = Mod.GetCard(newCardName);
            deck.Insert(index++, newCard.Clone());
            CardDiscoverSystem.instance.DiscoverCard(newCard);
        }
        deck.Remove(card);
    }
}