using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectApplyXOnHitScriptableTarget : StatusEffectApplyXOnHit
{
    public ScriptableAmount targetScriptableAmount;
    
    public override void Init()
    {
        if (postHit)
        {
            PostHit += CheckHit;
        }
        else
        {
            OnHit += CheckHit;
        }
    }

    private new IEnumerator CheckHit(Hit hit)
    {
        yield return Run(GetTargets(hit), targetScriptableAmount.Get(hit.target));
        storedHit.Remove(hit);
    }
}