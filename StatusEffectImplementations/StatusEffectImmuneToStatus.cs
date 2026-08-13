using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectImmuneToStatus : StatusEffectData
{
    public StatusEffectData replaceWith;
    public bool all = false;
    public bool debuffs = true;
    public string[] includeTypes = [];
    public string[] excludeTypes = [];

    public TargetConstraint[] conditions;
    
    public override bool RunApplyStatusEvent(StatusEffectApply apply)
    {
        if (apply.target == target && conditions.All(constraint => constraint.Check(target)) && CheckStatus(apply.effectData))
        {
            apply.effectData = replaceWith;
        }

        return false;
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
}