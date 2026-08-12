using System.Collections.Generic;
using System.Linq;

namespace PokemonMod.Scriptables.TargetConstraints;

public class TargetConstraintWeather : TargetConstraint
{
    public CardData weather;
    
    public override bool Check(Entity target)
    {
        List<Entity> entities = [.. target.GetAllAllies(), .. target.GetAllEnemies()];
        return entities.Any(entity => entity.data.name == weather.name) != not;
    }

    public override bool Check(CardData targetData)
    {
        return not;
    }
}