using System.Collections;
using PokemonMod.Helpers;
using UnityEngine;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectReduceDamageTaken : StatusEffectData
{
    public bool multiply;
    public float multiplier = 1f;
    public TargetConstraint[] constraints = [];
    public bool self = true;
    public bool allies;
    public bool enemies;


    public override void Init()
    {
        OnHit += Check;
    }

    public override bool RunHitEvent(Hit hit)
    {
        return CheckTarget(hit.target) && hit.damage > 0;
    }

    private bool CheckTarget(Entity entity)
    {
        if (!constraints.AllApply(entity))
        {
            return false;
        }
        if (self && target == entity)
        {
            return true;
        }
        if (allies && target != entity && target.owner == entity.owner)
        {
            return true;
        }

        return enemies && target.owner != entity.owner;
    }

    private IEnumerator Check(Hit hit)
    {
        if (multiply)
        {
            hit.damage = Mathf.CeilToInt(hit.damage * multiplier);
            yield break;
        }

        var block = GetAmount();
        hit.damage -= block;
        hit.damageBlocked += block;
    }
}