using System.Linq;

namespace PokemonMod.Scriptables.ScriptableAmounts;

internal class ScriptableDebuffs : ScriptableAmount
{
    public override int Get(Entity entity)
    {
        return entity.statusEffects.Sum(status => status.IsNegativeStatusEffect() ? status.count : 0);
    }
}