using System.Collections;
using System.Linq;
using UnityEngine;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantCountUpStatus : StatusEffectInstant
{
    public string[] types;
    public bool negative;
    public bool positive;

    public override IEnumerator Process()
    {
        var matchingStatus = target.statusEffects.Where(status =>
            status.isStatus && status.stackable &&
            (types.Length == 0 || types.Contains(status.type)) &&
            (positive != status.IsNegativeStatusEffect() ||
             negative == status.IsNegativeStatusEffect()));

        foreach (var status in matchingStatus.ToArray())
        {
            yield return CountUp(status);
        }

        yield return Remove();
    }

    private IEnumerator CountUp(StatusEffectData status)
    {
        yield return StatusEffectSystem.Apply(target, applier, status, count);
        yield return new WaitForSeconds(0.1f);
    }
}