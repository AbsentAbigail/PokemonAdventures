using System.Collections;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectAftershock : StatusEffectData
{
    public override void Init()
    {
        PostApplyStatus += Check;
    }

    public override bool RunPostApplyStatusEvent(StatusEffectApply apply)
    {
        var entity = apply.target;
        return apply.effectData.type == "vim" && entity.owner != target.owner;
    }

    private IEnumerator Check(StatusEffectApply apply)
    {
        target.curveAnimator.Ping();
        yield return Sequences.WaitForAnimationEnd(apply.target);
        var hit = new Hit(target, apply.target)
        {
            damage = 0, countsAsHit = true, Offensive = true, canRetaliate = false,
        };
        ActionQueue.Stack(new ActionSequence(hit.Process()));
    }
}