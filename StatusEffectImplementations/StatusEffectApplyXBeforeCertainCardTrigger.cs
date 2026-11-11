using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectApplyXBeforeCardTrigger : StatusEffectApplyX
{
    public TargetConstraint[] cardConstraints;
    public bool onlyAllied = true;
    public bool onSelf = false;
    public List<Entity> blockedEntities = [];
    
    public override void Init()
    {
        PreTrigger += Check;
        Events.OnActionFinished += ActionFinished;
    }

    private void ActionFinished(PlayAction arg0)
    {
        if (ActionQueue.Empty)
        {
            blockedEntities.Clear();
        }
    }

    public override bool RunPreTriggerEvent(Trigger trigger)
    {
        var entity = trigger.entity;

        if (blockedEntities.Contains(entity))
        {
            return false;
        }
        
        if (entity == target)
        {
            return false;
        }
        
        if (onSelf && (trigger.targets == null || !trigger.targets.Contains(target)))
        {
            return false;
        }

        if (cardConstraints.Any(constraint => !constraint.Check(entity)))
        {
            return false;
        }

        return !onlyAllied || entity.owner == target.owner;
    }

    private IEnumerator Check(Trigger trigger)
    {
        blockedEntities.Add(trigger.entity);
        yield return Run(GetTargets(new Hit(trigger.entity, null)));
    }
}