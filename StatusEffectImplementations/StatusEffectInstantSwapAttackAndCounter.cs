using System;
using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantSwapAttackAndCounter : StatusEffectInstant
{
    public override IEnumerator Process()
    {
        var previousDamage = target.damage.max;
        (target.damage.max, target.counter.max) = (target.counter.max, Math.Max(target.damage.max, 1));
        var difference = target.damage.max - previousDamage;
        target.damage.current += difference;
        target.counter.current = target.counter.max;
        
        yield return Remove();
    }
}