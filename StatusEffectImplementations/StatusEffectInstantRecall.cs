using System.Collections;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantRecall : StatusEffectInstant
{
    public bool forceRecall;
    
    public override IEnumerator Process()
    {
        if (AlreadyRecalling(target))
        {
            yield return Remove();
            yield break;
        }
        
        if (forceRecall || target.CanRecall())
        {
            var action = new ActionMove(target, References.Player.discardContainer);
            Events.InvokeDiscard(target);
            ActionQueue.Add(action);
        }
        else
        {
            yield return NoTargetTextSystem.Run(target, NoTargetType.CantMove);
        }
        
        yield return Remove();
    }

    private static bool AlreadyRecalling(Entity entity)
    {
        return ActionQueue.GetActions().Any(action => action is ActionMove move && move.entity == entity && move.toContainers.Contains(References.Player.discardContainer));
    }
}