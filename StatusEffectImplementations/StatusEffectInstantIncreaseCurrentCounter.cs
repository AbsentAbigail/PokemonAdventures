using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantIncreaseCurrentCounter : StatusEffectInstant
{
    public override IEnumerator Process()
    {
        target.counter.current += count;
        yield return base.Process();
    }
}