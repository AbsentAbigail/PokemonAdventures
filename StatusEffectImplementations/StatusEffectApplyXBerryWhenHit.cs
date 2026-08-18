using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectApplyXBerryWhenHit : StatusEffectApplyX
{
    public override void Init()
    {
        PostHit += Check;
    }

    public override bool RunPostHitEvent(Hit hit)
    {
        return target.enabled && hit.target == target && hit.canRetaliate && (!targetMustBeAlive || target.alive && Battle.IsOnBoard(target)) && hit.Offensive && hit.BasicHit;
    }

    private IEnumerator Check(Hit hit)
    {
        yield return Run(GetTargets(new Hit(hit.attacker, target)));
        yield return Remove();
        target.display.promptUpdateDescription = true;
        target.PromptUpdate();
    }
}