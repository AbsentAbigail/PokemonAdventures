using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantConsume : StatusEffectInstant
{
    public override IEnumerator Process()
    {
        target.forceKill = DeathType.Consume;
        yield return Remove();
    }
}