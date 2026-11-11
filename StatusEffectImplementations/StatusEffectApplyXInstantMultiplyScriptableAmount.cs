using System.Collections;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectApplyXInstantMultiplyScriptableAmount : StatusEffectApplyXInstant
{
    public override int GetAmount(Entity entity, bool equalAmount = false, int equalTo = 0)
    {
        if (scriptableAmount)
        {
            return scriptableAmount.Get(entity) * GetAmount();
        }
        return !equalAmount ? GetAmount() : equalTo;
    }
}