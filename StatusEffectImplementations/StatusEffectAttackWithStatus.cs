using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectAttackWithStatus : StatusEffectData
{
    public string attackWithType = Mod.GetStatus("Teeth").type;
    public string withDamageTypeAnimation = "spikes";

    public override void Init()
    {
        PreAttack += Check;
    }

    private IEnumerator Check(Hit hit)
    {
        if (hit.attacker != target)
        {
            yield break;
        }

        var attack = target.FindStatus(attackWithType)?.count ?? 0;

        hit.damage = attack;

        if (FindObjectOfType<VfxStatusSystem>() is { } hitSystem  && !withDamageTypeAnimation.IsNullOrEmpty())
        {
            hitSystem.EntityHit(new Hit(hit.attacker, hit.target, hit.damage)
            {
                damageType = withDamageTypeAnimation,
            });
        }
    }
}