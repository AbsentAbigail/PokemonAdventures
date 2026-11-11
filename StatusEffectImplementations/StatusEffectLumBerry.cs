using System.Collections;
using PokemonMod.Helpers;

namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectLumBerry : StatusEffectData
{
    private bool _isActive;
    public StatusEffectData doNothing;

    public override void Init()
    {
        OnApplyStatus += CancelDebuff;
        Events.OnActionFinished += CleanseAfter;
    }

    private void CleanseAfter(PlayAction _)
    {
        if (_isActive && ActionQueue.Empty)
        {
            ActionQueue.Add(new ActionSequence(CleanseAfter()));
        }
    }

    public override bool RunApplyStatusEvent(StatusEffectApply apply)
    {
        LogHelper.Log("Run apply status event " + apply.effectData.name);
        return apply.target == target && apply.effectData.IsNegativeStatusEffect() && apply.count > 0;
    }

    private IEnumerator CancelDebuff(StatusEffectApply apply)
    {
        LogHelper.Log("apply status event " + apply.effectData.name);
        
        apply.effectData = doNothing;
        if (_isActive)
        {
            yield break;
        }
        
        _isActive = true;
    }
    
    private IEnumerator CleanseAfter()
    {
        _isActive = false;
        for (var i = target.statusEffects.Count - 1; i >= 0; --i)
        {
            var statusEffect = target.statusEffects[i];
            if (statusEffect.IsNegativeStatusEffect())
            {
                yield return statusEffect.Remove();
            }
        }
        yield return CountDown(target, 1);
        target.display.promptUpdateDescription = true;
        target.PromptUpdate();
        target.display.UpdateDisplay();
    }
}