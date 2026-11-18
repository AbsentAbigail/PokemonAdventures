using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectRetaliateSoulbound : StatusEffectData
{
    private Hit _lastHit;

    public override void Init()
    {
        OnEntityDestroyed += CheckDestroy;
    }

    public override bool RunHitEvent(Hit hit)
    {
        if (hit.target == target && hit.Offensive)
        {
            _lastHit = hit;
        }
        return false;
    }

    public override bool RunEntityDestroyedEvent(Entity entity, DeathType deathType)
    {
        return entity == target;
    }

    private IEnumerator CheckDestroy(Entity entity, DeathType deathType)
    {
        var attacker = _lastHit.attacker;
        return new Hit(attacker, attacker, 100).Process();
    }
}