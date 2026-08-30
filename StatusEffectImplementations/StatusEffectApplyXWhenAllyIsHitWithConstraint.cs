using PokemonMod.Helpers;

namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectApplyXWhenAllyIsHitWithConstraint : StatusEffectApplyXWhenAllyIsHit
{
    public TargetConstraint[] constraints;
    
    public override bool RunPostHitEvent(Hit hit)
    {
        return base.RunPostHitEvent(hit) && constraints.AllApply(hit.target);
    }
}