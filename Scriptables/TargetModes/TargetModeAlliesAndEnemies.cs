using System.Collections.Generic;
using System.Linq;

namespace PokemonMod.Scriptables.TargetModes;

internal class TargetModeAlliesAndEnemies : TargetModeAll
{
    public override Entity[] GetPotentialTargets(Entity entity, Entity target, CardContainer targetContainer)
    {
        HashSet<Entity> hashSet =
        [
            .. from e in entity.GetAllEnemies()
            where (bool)e && e.enabled && e.alive && e.canBeHit && CheckConstraints(e)
            select e,
            .. from e in entity.GetAllAllies()
            where (bool)e && e.enabled && e.alive && e.canBeHit && CheckConstraints(e)
            select e,
        ];
        if (hashSet.Count <= 0)
        {
            return null;
        }

        return [.. hashSet];
    }
}