using System.Collections;
using PokemonMod.Helpers;

namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectApplyXWhenCardTakesDamage : StatusEffectApplyX
{
    public TargetConstraint[] constraints;
    public bool allies = true;
    public bool enemies = true;
    public bool self = true;

    public override void Init()
    {
        PostHit += CheckHit;
    }

    public override bool RunPostHitEvent(Hit hit)
    {
        if (hit.target == target && !self)
        {
            return false;
        }
        if (hit.target.owner == target.owner && !allies)
        {
            return false;
        }
        if (hit.target.owner != target.owner && !enemies)
        {
            return false;
        }
        
        return target.enabled && target.alive && hit.Offensive && Battle.IsOnBoard(target) && constraints.AllApply(hit.target);
    }

    private IEnumerator CheckHit(Hit hit) => Run(GetTargets(hit), hit.damage + hit.damageBlocked);
}