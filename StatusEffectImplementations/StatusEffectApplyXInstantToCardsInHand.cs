using System.Collections;
using System.Collections.Generic;
using PokemonMod.Helpers;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectApplyXInstantToCardsInHand : StatusEffectApplyXInstant
{
    public override void Init()
    {
        OnBegin += Check;
    }

    private IEnumerator Check()
    {
        List<Entity> targets = [];
        var found = GetAmount();
        if (GetAmount() <= 0)
        {
            yield return Remove();
            yield break;
        }
        
        foreach (var entity in References.Player.handContainer)
        {
            if (!applyConstraints.AllApply(entity))
            {
                continue;
            }
            
            targets.Add(entity);
            if (--found <= 0)
            {
                break;
            }
        }
        yield return Run(targets);
        yield return Remove();
    }
}