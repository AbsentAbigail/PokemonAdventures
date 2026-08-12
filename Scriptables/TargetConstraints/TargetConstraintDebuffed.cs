using System.Linq;

namespace PokemonMod.Scriptables.TargetConstraints;

public class TargetConstraintDebuffed : TargetConstraint
{
    public override bool Check(Entity target)
    {
        return target.statusEffects.Any(effect => effect.isStatus && effect.offensive) != not;
    }

    public override bool Check(CardData targetData)
    {
        return targetData.startWithEffects.Any(effect => effect.data.isStatus && effect.data.offensive) != not;
    }
}