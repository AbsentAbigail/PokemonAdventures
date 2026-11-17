using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectApplyXWhenCardRecalled : StatusEffectApplyX
{
    public bool onBoard = true;
    public bool self = false;
    public bool allies;
    public bool enemies;
    public TargetConstraint[] constraints = [];
    
    public int actionPriority = 0;

    public override void Init()
    {
        Events.OnDiscard += Check;
    }

    public void OnDestroy()
    {
        Events.OnDiscard -= Check;
    }

    private void Check(Entity discarded)
    {
        if (discarded.data.cardType.item)
        {
            return;
        }

        if (onBoard && !Battle.IsOnBoard(target))
        {
            return;
        }
        
        if (!CheckTarget(discarded))
        {
            return;
        }

        if (constraints.Any(constraint => !constraint.Check(discarded)))
        {
            return;
        }

        var hackyHit = new Hit(null, discarded);
        ActionQueue.Stack(new ActionSequence(Run(GetTargets(hackyHit))) {priority = actionPriority});
    }

    private bool CheckTarget(Entity entity)
    {
        if (self && entity == target)
        {
            return true;
        }

        if (allies && target.owner == entity.owner)
        {
            return true;
        }

        return enemies && target.owner != entity.owner;
    }
}