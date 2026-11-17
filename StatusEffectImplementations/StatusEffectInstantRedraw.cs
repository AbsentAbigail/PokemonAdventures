using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectInstantRedraw : StatusEffectInstant
{
    public override IEnumerator Process()
    {
        var discarded = DiscardNonLeaders();
        ActionQueue.Insert(++discarded, new ActionDraw(References.Player, References.Player.handContainer.max));
    
        yield return base.Process();
    }

    private static int DiscardNonLeaders()
    {
        var i = 0;
        foreach (var entity in References.Player.handContainer)
        {
            if (entity.data.cardType.miniboss)
            {
                continue;
            }
            entity.display.hover.SetHoverable(false);
            ActionQueue.Insert(i++, new ActionMove(entity, References.Player.discardContainer));
        }
        return i;
    }
}