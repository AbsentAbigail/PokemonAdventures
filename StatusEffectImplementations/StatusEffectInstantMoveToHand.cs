using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantMoveToHand : StatusEffectInstant
{
    public override IEnumerator Process()
    {
        yield return Sequences.WaitForAnimationEnd(target);
        Events.InvokeCardDraw(count);
        yield return Sequences.CardMove(target, [
            References.Player.handContainer,
        ]);
        Events.InvokeCardDrawEnd();
        ActionQueue.Stack(new ActionRevealAll(References.Player.handContainer));
        yield return base.Process();
    }
}