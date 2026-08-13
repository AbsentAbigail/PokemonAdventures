using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectApplyXWhenConstrictedDamageDealt : StatusEffectApplyX
{
    public bool ownConstrict = true;
    public bool alliedConstrict = false;
    public bool anyConstrict = false;
    public bool postHit = false;
    
    public override void Init()
    {
        if (postHit)
        {
            PostHit += Check;
            return;
        }
        
        OnHit += Check;
    }

    public override bool RunHitEvent(Hit hit)
    {
        return hit.damageType == "constricted" && CheckAttacker(hit);
    }

    public override bool RunPostHitEvent(Hit hit)
    {
        return RunHitEvent(hit);
    }

    private IEnumerator Check(Hit hit)
    {
        yield return Run(GetTargets(hit), hit.damage + hit.damageBlocked);
    }
    
    private bool CheckAttacker(Hit hit)
    {
        if (anyConstrict)
        {
            return true;
        }

        if (!hit.attacker)
        {
            return false;
        }
        
        if (ownConstrict && hit.attacker == target)
        {
            return true;
        }
        
        return alliedConstrict && hit.attacker != target && hit.attacker.owner == target.owner;
    }
}