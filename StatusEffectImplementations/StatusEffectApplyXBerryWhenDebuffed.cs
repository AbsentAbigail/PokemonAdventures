using System.Collections;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectApplyXBerryWhenDebuffed : StatusEffectApplyX
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
        yield return CountDown(target, 1);
        target.display.promptUpdateDescription = true;
        target.PromptUpdate();
    }
}