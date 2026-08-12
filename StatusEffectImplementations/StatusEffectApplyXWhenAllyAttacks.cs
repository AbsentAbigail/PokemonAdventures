namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectApplyXWhenAllyAttacks : StatusEffectApplyXWhenAlliesAttack
{
    public override bool RunPreAttackEvent(Hit hit)
    {
        return base.RunPreAttackEvent(hit) && Battle.IsOnBoard(target) && Battle.IsOnBoard(hit.attacker);
    }
}