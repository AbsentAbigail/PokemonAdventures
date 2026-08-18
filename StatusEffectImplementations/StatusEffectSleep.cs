using System.Collections;
using PokemonMod.GameSystems;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectSleep : StatusEffectData
{
    public bool primed;
    
    public override void Init()
    {
        PreTrigger += ReplaceTrigger;
        OnTurnEnd += CustomCountDown;
    }

    public override bool RunPreTriggerEvent(Trigger trigger)
    {
        return trigger.entity == target;
    }

    private IEnumerator ReplaceTrigger(Trigger trigger)
    {
        trigger.nullified = true;
        yield return CustomTextPopupSystem.RunWithShake(target, Mod.GetLocalizedString("Sleep"), target.data.title);
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