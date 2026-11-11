using System;
using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantSwapAttackAndCounter : StatusEffectInstant
{
    public int modifyAttack;
    public int modifyCounter;
    
    public override IEnumerator Process()
    {
        var previousDamage = target.damage.max;
        (target.damage.max, target.counter.max) = (target.counter.max + modifyCounter, Math.Max(target.damage.max + modifyAttack, 1));
        var difference = target.damage.max - previousDamage;
        target.damage.current += difference;
        target.counter.current = target.counter.max;
        
        yield return Remove();
    }
}