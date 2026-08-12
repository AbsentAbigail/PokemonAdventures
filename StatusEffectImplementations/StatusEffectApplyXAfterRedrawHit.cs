namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectApplyXAfterRedrawHit : StatusEffectApplyX
{
    public override void Init()
    {
        Events.OnRedrawBellHit += RedrawBellHit;
    }

    public void OnDestroy()
    {
        Events.OnRedrawBellHit -= RedrawBellHit;
    }

    public void RedrawBellHit(RedrawBellSystem redrawBellSystem)
    {
        if (!Battle.IsOnBoard(target) || !CanTrigger())
        {
            return;
        }
        ActionQueue.Add(new ActionSequence(Run(GetTargets())), true);
    }
}