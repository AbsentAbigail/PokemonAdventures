using System.Collections;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectWhileActiveXUpdateWhenMoved : StatusEffectWhileActiveX
{
    public override void Init()
    {
        base.Init();
        OnCardMove += Moved;
    }

    private IEnumerator Moved(Entity entity)
    {
        if (scriptableAmount)
        {
            var amount = scriptableAmount.Get(target);
            if (amount == currentAmount)
            {
                yield break;
            }
            
            yield return Deactivate();
            yield return Activate();

            yield break;
        }
        
        var preContainer = entity.preContainers.Any() ? entity.preContainers.First().Group : null;
        var container = entity.containers.Any() ? entity.containers.First().Group : null;
        if (preContainer == container)
        {
            yield break;
        }
        
        yield return Deactivate();
        yield return Activate();
    }
}