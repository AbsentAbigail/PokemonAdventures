using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectApplyXToTargetWhenYApplied : StatusEffectApplyXWhenYAppliedTo
{
    public override void Init()
    {
        PostApplyStatus += Apply;
    }

    private IEnumerator Apply(StatusEffectApply apply) => Run(GetTargets(new Hit(apply.applier, apply.target)), apply.count);
}