using System.Collections;
using System.Linq;
using PokemonMod.Helpers;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectImmuneToStatus : StatusEffectData
{
    public StatusEffectData replaceWith;
    public bool all = false;
    public bool debuffs = true;
    public bool ignoreInk = false;
    public string[] includeTypes = [];
    public string[] excludeTypes = [];

    public TargetConstraint[] conditions;
    
    private bool _removeAfterInk = false;
    
    public override void Init()
    {
        OnEffectBonusChanged += Clear;
    }

    public override bool RunApplyStatusEvent(StatusEffectApply apply)
    {
        if (apply.target == target && conditions.All(constraint => constraint.Check(target)) && CheckStatus(apply.effectData) && CheckInk())
        {
            apply.effectData = replaceWith;
        }

        return false;
    }
    
    public override bool RunEffectBonusChangedEvent()
    {
        if (!_removeAfterInk)
        {
            return false;
        }

        if (target.silenced)
        {
            return false;
        }


        _removeAfterInk = false;
        return true;
    }

    private IEnumerator Clear()
    {
        for (var i = target.statusEffects.Count - 1; i >= 0; --i)
        {
            var statusEffect = target.statusEffects[i];
            if (CheckStatus(statusEffect))
            {
                yield return statusEffect.Remove();
            }
        }
    }

    private bool CheckStatus(StatusEffectData status)
    {
        if (all)
        {
            return !excludeTypes.Contains(status.type);
        }
        
        if (includeTypes.Length > 0 && includeTypes.Contains(status.type))
        {
            return true;
        }

        if (debuffs && status.IsNegativeStatusEffect())
        {
            return !excludeTypes.Contains(status.type);
        }
        
        return false;
    }

    private bool CheckInk()
    {
        if (ignoreInk || !target.silenced)
        {
            return true;
        }
        
        _removeAfterInk = true;
        return false;
    }
}