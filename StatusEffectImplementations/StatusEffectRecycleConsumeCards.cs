using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DeadExtensions;
using HarmonyLib;
using PokemonMod.GameSystems;
using UnityEngine;
using UnityEngine.Localization;
using WildfrostHopeMod.Utils;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectRecycleConsumeCards : StatusEffectData
{
    public StatusEffectInstantSummon summonCopy;
    public LocalizedString title;

    private CardContainer _cardContainer;
    private GameObject _gameObject;
    private GameObject _objectGroup;

    private Entity _selected;
    private CardPocketSequence _sequence;

    public override void Init()
    {
        OnCardPlayed += Played;
    }
    
    public override bool RunCardPlayedEvent(Entity entity, Entity[] targets)
    {
        var tracker = FindObjectOfType<ConsumedItemTrackerSystem>();
        return target.enabled && entity == target && tracker.ConsumedCards.Count > 0;
    }

    private IEnumerator Played(Entity entity, Entity[] targets)
    {
        _sequence = FindObjectOfType<CardPocketSequence>(true);
        var cc = (CardControllerSelectCard)_sequence.cardController;
        cc.pressEvent.AddListener(ChooseCard);
        cc.canPress = true;
        var container = GetCardContainer();
        foreach (var containerEntity in container)
        {
            yield return containerEntity.GetCard()!.UpdateData();
        }

        CinemaBarSystem.In();
        CinemaBarSystem.SetSortingLayer("UI2");
        if (!title.IsEmpty)
        {
            CinemaBarSystem.Top.SetPrompt(title.GetLocalizedString(), "Select");
        }
        _sequence.AddCards(container);
        yield return _sequence.Run();

        // if (_selected != null) //Card Selected
        // {
        //     Events.InvokeCardDraw(1);
        //     yield return Sequences.CardMove(_selected, [References.Player.handContainer]);
        //     References.Player.handContainer.TweenChildPositions();
        //     Events.InvokeCardDrawEnd();
        //     _selected.flipper.FlipUp();
        //     yield return Sequences.WaitForAnimationEnd(_selected);
        //     yield return new ActionRunEnableEvent(_selected).Run();
        //     _selected.display.hover.enabled = true;
        //
        //     _selected.display.promptUpdateDescription = true;
        //     _selected.PromptUpdate();
        //
        //     ActionQueue.Stack(new ActionSequence(_selected.UpdateTraits()) { note = $"[{_selected}] Update Traits" });
        //
        //     _selected = null;
        // }

        _cardContainer?.ClearAndDestroyAllImmediately();

        cc.canPress = false;
        cc.pressEvent.RemoveListener(ChooseCard);

        CinemaBarSystem.Clear();
        CinemaBarSystem.Out();

        yield return Remove();
    }

    private void ChooseCard(Entity entity)
    {
        _selected = entity;
        _sequence.promptEnd = true;

        var cardData = _selected.data;
        summonCopy.targetSummon.summonCard = cardData;
        ActionQueue.Stack(new ActionApplyStatus(target, target, summonCopy, count));

        _selected = null;
    }

    private CardContainer GetCardContainer()
    {
        _objectGroup = new GameObject("SelectCardRoutine");
        _objectGroup.SetActive(false);
        _objectGroup.transform.SetParent(GameObject.Find("Canvas/Padding/HUD/DeckpackLayout").transform.parent.GetChild(0));
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
        var tracker = FindObjectOfType<ConsumedItemTrackerSystem>();
        foreach (var cardName in InPettyRandomOrder(tracker.ConsumedCards))
        {
            var cardData = Mod.GetCard(cardName).Clone();
            var card = CardManager.Get(cardData, Battle.instance.playerCardController, References.Player,
                true,
                true);
            _cardContainer.Add(card.entity);
        }
    }

    // Random Order from Pokefrost StatusEffectChangeData
    private static IOrderedEnumerable<T> InPettyRandomOrder<T>(IEnumerable<T> source)
    {
        return source.OrderBy(_ => Dead.PettyRandom.Range(0f, 1f));
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