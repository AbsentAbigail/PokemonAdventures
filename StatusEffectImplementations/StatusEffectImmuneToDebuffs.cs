namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectImmuneToDebuffs : StatusEffectData
{
    public StatusEffectData replaceWith;
    
    public override void Init()
    {
      
    }

    public override bool RunApplyStatusEvent(StatusEffectApply apply)
    {
        if (!apply.effectData.IsNegativeStatusEffect())
        {
            return false;
        }
        apply.effectData = replaceWith;
        return false;
    }
}