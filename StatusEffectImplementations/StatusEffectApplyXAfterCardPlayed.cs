using System.Collections;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectApplyXAfterCardPlayed : StatusEffectApplyX
{
    public override void Init()
    {
        OnCardPlayed += Check;
    }

    private IEnumerator Check(Entity entity, Entity[] targets)
    {
        yield return Run(GetTargets());
    }

    public override bool RunCardPlayedEvent(Entity entity, Entity[] targets)
    {
        if (entity != target)
        {
            return false;
        }

        if (target.silenced)
        {
            return false;
        }

        var hasQueuedTriggers = ActionQueue.GetActions()
            .Any(playAction => playAction is ActionTrigger actionTrigger && actionTrigger.entity == target);
        return !hasQueuedTriggers;
    }
}