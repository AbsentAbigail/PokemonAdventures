using System.Linq;

namespace PokemonMod.Scriptables.TargetConstraints;

public class TargetConstraintCanEvolve : TargetConstraint
{
    public override bool Check(Entity target)
    {
        return Check(target.data);
    }

    public override bool Check(CardData targetData)
    {
        return Evolutions.Profiles.Any(profile => profile.cardName == targetData.name) != not;
    }
}