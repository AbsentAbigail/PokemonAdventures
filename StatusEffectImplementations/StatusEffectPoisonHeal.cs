using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectPoisonHeal : StatusEffectData
{
    public bool heal;
    
    public override void Init()
    {
        if (heal)
        {
            OnHit += Heal;
            return;
        }
        
        OnHit += Nullify;
    }

    public override bool RunHitEvent(Hit hit)
    {
        return hit.target == target && hit.damageType == "shroom";
    }

    private static IEnumerator Heal(Hit hit)
    {
        hit.countsAsHit = false;
        hit.Offensive = false;
        hit.damage = -(hit.damage + hit.damageBlocked);
        hit.damageBlocked = 0;
        yield break;
    }
    
    private static IEnumerator Nullify(Hit hit)
    {
        hit.damage = 0;
        hit.nullified = true;
        yield break;
    }
}