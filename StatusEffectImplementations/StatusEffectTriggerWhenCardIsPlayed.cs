using System.Collections.Generic;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectTriggerWhenCardIsPlayed : StatusEffectReaction
{
    public bool againstTarget;
    public CardData[] allowedCards;
    
    private readonly HashSet<Entity> _prime = [];

    public override bool RunHitEvent(Hit hit)
    {
        if (!target.enabled || !Battle.IsOnBoard(target))
        {
            return false;
        }
        
        if ((hit.countsAsHit || hit.Offensive || hit.target && hit.trigger != null) && CheckEntity(hit.attacker))
        {
            _prime.Add(hit.attacker);
        }
        
        return false;
    }

    public override bool RunCardPlayedEvent(Entity entity, Entity[] targets)
    {
        if (_prime.Count <= 0 || !_prime.Contains(entity) || targets is not { Length: > 0 })
        {
            return false;
        }
        
        if (!Battle.IsOnBoard(target) || !CanTrigger())
        {
            return false;
        }
        
        _prime.Remove(entity);
        Run(entity, targets);
        return false;
    }

    private void Run(Entity attacker, Entity[] targets)
    {
        if (!againstTarget)
        {
            ActionQueue.Stack(new ActionTrigger(target, attacker), true);
            return;
        }
        
        foreach (var entity in targets)
        {
            ActionQueue.Stack(new ActionTriggerAgainst(target, attacker, entity, null), true);
        }
    }

    private bool CheckEntity(Entity entity)
    {
        return entity && allowedCards.Any(allowedCard => allowedCard.name == entity.data.name)
                      && entity.owner.team == target.owner.team && entity != target;
    }
}