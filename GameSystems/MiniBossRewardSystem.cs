using System.Collections;
using System.Linq;
using HarmonyLib;
using PokemonMod.Builders.Cards.Items;
using UnityEngine;
using UnityEngine.Localization;
using WildfrostHopeMod.Utils;

namespace PokemonMod.GameSystems;

[HarmonyPatch]
public class MiniBossRewardSystem : GameSystem
{
    private static readonly string CardName = Item.Name;
    private static readonly LocalizedString Title = Mod.GetLocalizedString("MinibossReward");

    private static Entity _selected;
    private static CardContainer _cardContainer;
    private static GameObject _gameObject;
    private static GameObject _objectGroup;
    private static CardPocketSequence _sequence;
    
    public IEnumerator ChooseRewards(CardUpgradeData[] choices)
    {
        _sequence = FindObjectOfType<CardPocketSequence>(true);
        var cardController = (CardControllerSelectCard)_sequence.cardController;
        cardController.pressEvent.AddListener(ChooseCard);
        cardController.canPress = true;
        var container = GetCardContainer(choices);

        foreach (var card in container)
        {
            yield return card.GetCard()!.UpdateData();
        }

        CinemaBarSystem.In();
        CinemaBarSystem.SetSortingLayer("UI2");
        if (!Title.IsEmpty)
        {
            CinemaBarSystem.Top.SetPrompt(Title.GetLocalizedString(), "Select");
        }
        _sequence.AddCards(container);
        yield return _sequence.Run();
        
        _cardContainer?.ClearAndDestroyAllImmediately();

        cardController.canPress = false;
        cardController.pressEvent.RemoveListener(ChooseCard);

        CinemaBarSystem.Clear();
        CinemaBarSystem.Out();
    }
    
    
    private static void ChooseCard(Entity entity)
    {
        _selected = entity;
        _sequence.promptEnd = true;

        var cardData = _selected.data;
        var upgradeInventory = References.PlayerData.inventory.upgrades;
        upgradeInventory.AddRange(cardData.upgrades.Select(upgrade =>
        {
            CardDiscoverSystem.instance.DiscoverCharm(upgrade.name);
            return upgrade.Clone();
        }));
        _selected = null;
    }

    private static CardContainer GetCardContainer(CardUpgradeData[] choices)
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

        FillCardContainer(Mod.GetCard(CardName), choices);

        _cardContainer.AssignController(Battle.instance.playerCardController);

        return _cardContainer;
    }

    private static void FillCardContainer(CardData itemCard, CardUpgradeData[] choices)
    {
        foreach (var cardUpgradeData in choices)
        {
            var cardData = itemCard.Clone();
            cardData.forceTitle = "Item: " + cardUpgradeData.title;
            cardData.textInsert = cardUpgradeData.text;
            cardData.upgrades.Add(cardUpgradeData.Clone());
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