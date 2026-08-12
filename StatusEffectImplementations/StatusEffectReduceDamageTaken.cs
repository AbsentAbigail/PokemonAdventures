using System.Collections;
using UnityEngine;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectReduceDamageTaken : StatusEffectData
{
    public bool multiply = false;
    public float multiplier = 1f;
    
    public override void Init()
    {
        OnHit += Check;
    }

    public override bool RunHitEvent(Hit hit)
    {
        return hit.target == target && hit.damage > 0;
    }

    private IEnumerator Check(Hit hit)
    {
        if (multiply)
        {
            hit.damage = Mathf.CeilToInt(hit.damage * multiplier);
            yield break;
        }
        
        var block = GetAmount();
        hit.damage -= block;
        hit.damageBlocked += block;
    }
}