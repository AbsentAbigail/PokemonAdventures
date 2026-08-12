using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectStanceChange : StatusEffectData
{
    public CardData.StatusEffectStacks[] firstStance;
    public CardData.StatusEffectStacks[] secondStance;

    public bool isFirstStance = true;
    
    public override void Init()
    {
        OnCardPlayed += Swap;
        OnStack += Stack;
    }

    private IEnumerator Stack(int stacks)
    {
        if (BattleSaveSystem.instance?.loading ?? false)
        {
            yield break;
        }

        var activeStance = isFirstStance ? firstStance : secondStance;
        foreach (var stack in activeStance)
        {
            yield return StatusEffectSystem.Apply(target, target, stack.data, stack.count);
        }
        
        target.display.promptUpdateDescription = true;
        target.PromptUpdate();
    }

    public override bool RunCardPlayedEvent(Entity entity, Entity[] targets)
    {
        return target.enabled && entity == target && !ActionQueue.GetActions().Any(action => action is ActionTrigger actionTrigger && actionTrigger.entity == target);
    }
    
    private IEnumerator Swap(Entity entity, Entity[] targets)
    {
        isFirstStance = !isFirstStance;
        var activeStance = isFirstStance ? firstStance : secondStance;
        var inactiveStance = !isFirstStance ? firstStance : secondStance;

        var clump = new Routine.Clump();
        foreach (var stack in inactiveStance)
        {
            var effect = target.statusEffects.Find(status => status.name == stack.data.name);
            clump.Add(effect.RemoveStacks(stack.count, false));
        }
        yield return clump.WaitForEnd();
        foreach (var stack in activeStance)
        {
            yield return StatusEffectSystem.Apply(target, target, stack.data, stack.count);
        }
        
        target.display.promptUpdateDescription = true;
        target.PromptUpdate();
    }
}