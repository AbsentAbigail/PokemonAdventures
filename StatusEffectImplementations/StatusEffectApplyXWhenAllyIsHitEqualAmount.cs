using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectApplyXWhenAllyIsHitEqualAmount : StatusEffectApplyXWhenAllyIsHit
{
    public override void Init()
    {
        PostHit += CheckHit;
    }

    private IEnumerator CheckHit(Hit hit)
    {
        yield return Run(GetTargets(hit), hit.damage + hit.damageBlocked);
    }
}