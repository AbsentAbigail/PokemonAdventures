using System.Collections;
using PokemonMod.GameSystems;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectSleep : StatusEffectData
{
    public bool primed;
    
    public override void Init()
    {
        Events.OnEntityPreTrigger += ReplaceTrigger;
        OnTurnEnd += CustomCountDown;
    }

    private void OnDestroy()
    {
        Events.OnEntityPreTrigger -= ReplaceTrigger;
    }

    private void ReplaceTrigger(ref Trigger trigger)
    {
        if (trigger.entity != target)
        {
            return;
        }

        ActionQueue.Insert(0, new ActionSequence(CustomTextPopupSystem.RunWithShake(target, Mod.GetLocalizedString("Sleep"), target.data.title)));
        trigger.nullified = true;
    }
    
    public override bool RunTurnStartEvent(Entity entity)
    {
        if (!primed && entity == target && Battle.IsOnBoard(entity))
        {
            primed = true;
        }
        return false;
    }

    public override bool RunTurnEndEvent(Entity entity)
    {
        return entity == target && primed;
    }

    private IEnumerator CustomCountDown(Entity entity)
    {
        var amount = 1;
        Events.InvokeStatusEffectCountDown(this, ref amount);
        if (amount == 0)
        {
            yield break;
        }

        yield return CountDown(entity, amount);
        entity.display.promptUpdateDescription = true;
        entity.PromptUpdate();
    }
}