using System.Collections;

namespace PokemonMod.Scriptables.CardScripts;


public class WonderTradeScript : CardScript
{
    private static readonly string[] WonderTradeOptions =
    [
        "Pichu",
        "Igglybuff",
        "Munchlax",
        
        "Celebi",
    ];
    private static CardControllerDeck _cardController;
    
    public override void Run(CardData target)
    {
        _cardController = FindObjectOfType<CardControllerDeck>();
        
        CardScriptDestroyCard.RemoveFromDeck(target);
        CardScriptDestroyCard.DestroyEntities(target);

        var cardData = Mod.GetCard(WonderTradeOptions.RandomItem());
        
        References.PlayerData.inventory.deck.Add(cardData);
        var card = CardManager.Get(cardData, _cardController, References.Player,
            false,
            true);
        References.instance.StartCoroutine(Animation(card));
    }

    // Copied from Pokefrosts CardScriptCopy
    // ReSharper disable Unity.PerformanceAnalysis
    private static IEnumerator Animation(Card card)
    {
        yield return card.UpdateData();

        var deckDisplaySequence = FindObjectOfType<DeckDisplaySequence>();
        if (deckDisplaySequence is null)
        {
            yield break;
        }

        deckDisplaySequence.activeCardsGroup.GetGrid(card).Add(card.entity);
        
        deckDisplaySequence.UpdatePositions();
    }
}