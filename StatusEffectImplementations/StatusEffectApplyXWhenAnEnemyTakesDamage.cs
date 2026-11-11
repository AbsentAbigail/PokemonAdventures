using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectApplyXWhenAnEnemyTakesDamage : StatusEffectApplyX
{
    public string targetDamageType = "shroom";

    public override void Init()
    {
        PostHit += CheckHit;
    }

    public override bool RunPostHitEvent(Hit hit)
    {
        return target.enabled && target.alive && hit.Offensive && hit.damageType == targetDamageType && Battle.IsOnBoard(target) && hit.target.owner != target.owner;
    }

    private IEnumerator CheckHit(Hit hit) => Run(GetTargets(hit), hit.damage + hit.damageBlocked);
}
