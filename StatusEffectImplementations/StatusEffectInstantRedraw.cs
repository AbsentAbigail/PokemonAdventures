using System.Collections;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectInstantRedraw : StatusEffectInstant
{
    public override IEnumerator Process()
    {
        yield return DiscardNonLeaders();
        yield return new ActionDraw(References.Player, References.Player.handContainer.max).Run();
    
        yield return base.Process();
    }

    private static IEnumerator DiscardNonLeaders()
    {
        var discard = References.Player.handContainer.Where(entity => !entity.data.cardType.miniboss).ToArray();
        
        foreach (var entity in discard)
        {
            entity.display.hover.SetHoverable(false);
            yield return new ActionMove(entity, References.Player.discardContainer).Run();
        }
    }
}