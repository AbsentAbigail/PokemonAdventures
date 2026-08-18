using System.Collections;
using System.Linq;
using PokemonMod.Variables;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;
using WildfrostHopeMod.Utils;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantChooseEvolution : StatusEffectInstant
{
    private CardData[] GetPossibleEvolutions()
    {
        return (from profile in Evolutions.Profiles where target.name == profile.cardName select Mod.GetCard(profile.changeToCardName).Clone()).ToArray();
    }
    
    public StatusEffectInstantEvolve evolveEffect;
    public LocalizedString title;

    private CardContainer _cardContainer;
    private GameObject _gameObject;
    private GameObject _objectGroup;

    private Entity _selected;
    private CardPocketSequence _sequence;

    public override IEnumerator Process()
    {
        _sequence = FindObjectOfType<CardPocketSequence>(true);
        var cardController = (CardControllerSelectCard)_sequence.cardController;
        cardController.pressEvent.AddListener(ChooseCard);
        cardController.canPress = true;

        var button = _sequence.backButton.GetComponentInChildren<Button>();
        button.onClick.AddListener(Return);
        
        var container = GetCardContainer();

        foreach (var entity in container)
        {
            yield return entity.GetCard()!.UpdateData();
        }
        
        CinemaBarSystem.In();
        CinemaBarSystem.SetSortingLayer("UI2");
        if (!title.IsEmpty)
        {
            CinemaBarSystem.Top.SetPrompt(title.GetLocalizedString().Format(target.data.title), "Select");
        }
        _sequence.AddCards(container);
        yield return _sequence.Run();

        if (_selected != null) //Card Selected
        {
            ActionQueue.Stack(new ActionApplyStatus(target, applier, evolveEffect, count));

            _selected = null;
        }

        _cardContainer?.ClearAndDestroyAllImmediately();

        cardController.canPress = false;
        cardController.pressEvent.RemoveListener(ChooseCard);
        button.onClick.RemoveListener(Return);

        CinemaBarSystem.Clear();
        CinemaBarSystem.Out();

        yield return Remove();
    }

    private void Return()
    {
        applier.owner.freeAction = true;
        var actionToRemove = ActionQueue.GetActions().FirstOrDefault(action =>
            action is ActionReduceUses reduceUses && reduceUses.entity == applier);
        ActionQueue.Remove(actionToRemove);
    }

    private void ChooseCard(Entity entity)
    {
        _selected = entity;
        _sequence.promptEnd = true;

        evolveEffect.evolveInto = _selected.data;
        
        ActionQueue.Stack(new ActionApplyStatus(target, applier, evolveEffect, count));

        _selected = null;
    }

    private CardContainer GetCardContainer()
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

        _cardContainer.AssignController(Battle.instance.playerCardController);

        return _cardContainer;
    }

    private void FillCardContainer()
    {
        foreach (var cardData in GetPossibleEvolutions())
        {
            cardData.cardType = target.data.cardType;
            cardData.SetCustomData("OverrideCardType", cardData.cardType.name);
            
            var card = CardManager.Get(cardData, Battle.instance.playerCardController, References.Player,
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
}