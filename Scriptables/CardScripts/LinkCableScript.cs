using System;
using System.Collections;
using System.Linq;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;
using UnityEngine;
using UnityEngine.Localization;
using WildfrostHopeMod.Utils;
using Random = Dead.Random;

namespace PokemonMod.Scriptables.CardScripts;


public class LinkCableScript : CardScript
{
    private static readonly LocalizedString Title = Mod.GetLocalizedString("LinkCable");
    private static readonly Predicate<CardData> Predicate = card => !card.cardType.miniboss && card.startWithEffects.Any(status => status.data is StatusEffectType) && card.mainSprite?.name != "Nothing";
    private const int Options = 3;

    private static Entity _selected;
    private static CardContainer _cardContainer;
    private static GameObject _gameObject;
    private static GameObject _objectGroup;
    private static CardPocketSequence _sequence;
    private static CardController _cardController;
    
    public override void Run(CardData target)
    {
        References.instance.StartCoroutine(Trade(target));
    }

    private static IEnumerator Trade(CardData target)
    {
        _cardController = FindObjectOfType<CardControllerDeck>();
        
        _sequence = FindObjectOfType<CardPocketSequence>(true);
        var backButtonScale = _sequence.backButton.transform.localScale; 
        _sequence.backButton.transform.localScale = new Vector3(0,0,0);
        var cardController = (CardControllerSelectCard)_sequence.cardController;
        cardController.pressEvent.AddListener(ChooseCard);
        cardController.canPress = true;
        var container = GetCardContainer();

        foreach (var entity in container)
        {
            yield return entity.GetCard()!.UpdateData();
        }

        CinemaBarSystem.In();
        CinemaBarSystem.SetSortingLayer("UI2");
        if (!Title.IsEmpty)
        {
            CinemaBarSystem.Top.SetPrompt(Title.GetLocalizedString(), "Select");
        }
        _sequence.AddCards(container);
        yield return _sequence.Run();
        
        var cardData = _selected.data.Clone();
        References.PlayerData.inventory.deck.Add(cardData);
        
        var card = CardManager.Get(cardData, _cardController, References.Player,
            false,
            true);
        References.instance.StartCoroutine(Animation(target, card));
        
        _selected = null;
        
        _cardContainer?.ClearAndDestroyAllImmediately();

        cardController.canPress = false;
        cardController.pressEvent.RemoveListener(ChooseCard);
        _sequence.backButton.transform.localScale = backButtonScale;
        
        CinemaBarSystem.Clear();
        CinemaBarSystem.Out();
    }
    
    private static void ChooseCard(Entity entity)
    {
        _selected = entity;
        _sequence.promptEnd = true;
    }

    private static CardContainer GetCardContainer()
    {
        _objectGroup = new GameObject("SelectCardRoutine");
        _objectGroup.SetActive(false);
        _objectGroup.transform.SetParent(GameObject.Find("Canvas/Padding/HUD/DeckpackLayout").transform.parent
            .GetChild(0));
        _objectGroup.transform.SetAsFirstSibling();

        _gameObject = new GameObject("SelectCard");
        var rect = _gameObject.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(7, 2);

        _cardContainer = CreateCardGrid(_objectGroup.transform, rect);

        FillCardContainer();

        _cardContainer.AssignController(_cardController);

        return _cardContainer;
    }

    private static void FillCardContainer()
    {
        var cards = AddressableLoader.GetGroup<CardData>("CardData")
            .Where(c => Predicate.Invoke(c))
            .OrderBy(_ => Random.Range(0f, 1f)).ToList().Take(Options);
        
        foreach (var c in cards)
        {
            var cardData = c.Clone();
            cardData.cardType = Mod.GetCardType("Friendly");
            cardData.SetCustomData("OverrideCardType", "Friendly");
            
            var card = CardManager.Get(cardData, _cardController, References.Player,
                true,
                true);
            _cardContainer.Add(card.entity);
        }
    }

    // Card Grid Code by Phan
    private static CardContainerGrid CreateCardGrid(Transform parent, RectTransform bounds = null)
    {
        return CreateCardGrid(parent, new Vector2(2.25f, 3.375f), 5, bounds);
    }

    private static CardContainerGrid CreateCardGrid(Transform parent, Vector2 cellSize, int columnCount,
        RectTransform bounds = null)
    {
        var gridObj = new GameObject("CardGrid", typeof(RectTransform), typeof(CardContainerGrid));
        gridObj.transform.SetParent(bounds ?? parent);
        gridObj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        var grid = gridObj.GetComponent<CardContainerGrid>();
        grid.holder = grid.GetComponent<RectTransform>();
        grid.onAdd = new UnityEventEntity(); // Fix null reference
        grid.onAdd.AddListener(entity =>
            entity.flipper.FlipUp()); // Flip up card when it's time (without waiting for others)
        grid.onRemove = new UnityEventEntity(); // Fix null reference

        grid.cellSize = cellSize;
        grid.columnCount = columnCount;

        AddScrollers(gridObj); // No click-and-drag. That needs Scroll View
        var scroller = gridObj.GetOrAdd<Scroller>();
        scroller.bounds = bounds; // Change scroller.bounds here if it only scrolls partially

        return grid;
    }

    /// <summary>
    ///     Generic way to make scrollable. Click-and-drag uses ScrollView
    /// </summary>
    /// <param name="parentObject"></param>
    private static void AddScrollers(GameObject parentObject)
    {
        var scroller = parentObject.GetOrAdd<Scroller>(); // Scroll with mouse
        parentObject.GetOrAdd<ScrollToNavigation>().scroller = scroller; // Scroll with controllers
        parentObject.GetOrAdd<TouchScroller>().scroller = scroller; // Scroll with touchscreen
    }
    
        // Copied from Pokefrosts CardScriptCopy
        // ReSharper disable Unity.PerformanceAnalysis
    private static IEnumerator Animation(CardData target, Card card)
    {
        var original = FindObjectsOfType<Entity>().FirstOrDefault(entity => entity.data == target);
        var position = original!.gameObject.transform.position;
        // var animation = VFXHelper.VFX.TryPlayEffect("trade", position, Vector3.zero);
        // LeanTween.scale(animation, Vector3.one, 0.1f);
        var cardSize = card.transform.localScale;
        LeanTween.scale(card.gameObject, Vector3.zero, 0f);
        LeanTween.move(card.gameObject, position, 0f); 
        LeanTween.scale(original.gameObject, Vector3.zero, 0.5f);
        yield return new WaitForSeconds(0.2f);

        CardScriptDestroyCard.RemoveFromDeck(target);
        original.RemoveFromContainers();
        original.display.GetCanvas().sortingLayerName = "Inspect";

        yield return card.UpdateData();
        LeanTween.scale(card.gameObject, cardSize, 0.5f);
        yield return new WaitForSeconds(0.5f);
        card.entity.curveAnimator.Ping();


        var deckDisplaySequence = FindObjectOfType<DeckDisplaySequence>();
        if (deckDisplaySequence is null)
        {
            yield break;
        }

        deckDisplaySequence.activeCardsGroup.GetGrid(card).Add(card.entity);

        CardManager.ReturnToPool(original.gameObject.GetComponent<Card>());
        deckDisplaySequence.UpdatePositions();
    }
}