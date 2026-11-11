using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectApplyXWhenDebuffed : StatusEffectApplyX
{
    public override void Init()
    {
        PostApplyStatus += Check;
    }

    public override bool RunPostApplyStatusEvent(StatusEffectApply apply)
    {
        return apply.target == target && apply.effectData.IsNegativeStatusEffect();
    }

    private IEnumerator Check(StatusEffectApply apply)
    {
        yield return Run(GetTargets(new Hit(apply.applier, target)));
    }
}