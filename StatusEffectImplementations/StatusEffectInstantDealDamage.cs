using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantDealDamage : StatusEffectApplyXInstant
{
    public override void Init()
    {
        OnBegin += Process;
    }

    private new IEnumerator Process()
    {
        var targets = GetTargets();
        var amount = GetAmount(target, applyEqualAmount);
        
        var clump = new Routine.Clump();
        foreach (var entity in targets)
        {
            var hit = new Hit(applier, entity, amount)
            {
                canRetaliate = canRetaliate,
                countsAsHit = countsAsHit,
            };
            clump.Add(hit.Process());
        }
        yield return clump.WaitForEnd();
        
        yield return Remove();
    }
}