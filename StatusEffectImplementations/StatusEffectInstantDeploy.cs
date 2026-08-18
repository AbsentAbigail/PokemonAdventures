using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantDeploy : StatusEffectInstant
{
    public CardData[] withCards;
    private readonly List<CardData> _pool = [];

    public override IEnumerator Process()
    {
        target.curveAnimator.Ping();
        var rows = References.Battle.GetRows(target.owner);
        var cardSlotList = new List<CardSlot>();
        foreach (var cardContainer in rows)
        {
            if (cardContainer is CardSlotLane cardSlotLane)
            {
                cardSlotList.AddRange(cardSlotLane.slots.Where(slot => slot.Empty));
            }
        }
        
        var i = 0;
        foreach (var slot in cardSlotList)
        {
            var card = CardManager.Get(Pull().Clone(), References.Battle.playerCardController, target.owner, true, target.owner.team == References.Player.team);
            yield return card.UpdateData();
            target.owner.reserveContainer.Add(card.entity);
            target.owner.reserveContainer.SetChildPosition(card.entity);
            ActionQueue.Stack(new ActionMove(card.entity, slot), true);
            ActionQueue.Stack(new ActionRunEnableEvent(card.entity), true);
            
            if (++i >= GetAmount())
            {
                break;
            } 
        }
        yield return base.Process();
    }

    private CardData Pull()
    {
        if (_pool.Count <= 0)
        {
            _pool.AddRange(withCards);
        }
        var cardData = _pool.TakeRandom();
        cardData.cardType = target.data.cardType;
        return cardData;
    }
}