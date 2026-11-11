using System.Collections;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectInstantDownload : StatusEffectInstant
{
    public override IEnumerator Process()
    {
        var copy = target.statusEffects.Where(status => status.isStatus && !status.IsNegativeStatusEffect());

        foreach (var statusEffectData in copy)
        {
            yield return StatusEffectSystem.Apply(applier, applier, statusEffectData, statusEffectData.count);
        }
    }
}