using System.Collections;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectApplyXWhenCertainCardPlayed : StatusEffectApplyX
{
    public TargetConstraint[] cardConstraints;
    public bool allies = true;
    
    public override void Init()
    {
        OnCardPlayed += Check;
    }

    public override bool RunCardPlayedEvent(Entity entity, Entity[] targets)
    {
        if (entity == target)
        {
            return false;
        }

        if (cardConstraints.Any(constraint => !constraint.Check(entity)))
        {
            return false;
        }

        return !allies || entity.owner == target.owner;
    }
    
    private IEnumerator Check(Entity entity, Entity[] targets)
    {
        yield return Run(GetTargets());
    }
}