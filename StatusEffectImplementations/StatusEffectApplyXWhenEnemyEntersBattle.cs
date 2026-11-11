namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectApplyXWhenEnemyEntersBattle : StatusEffectApplyX
{
    public override void Init()
    {
        Events.OnEntityEnabled += Check;
    }

    private void OnDestroy()
    {
        Events.OnEntityEnabled -= Check;
    }

    private void Check(Entity entity)
    {
        if (!Battle.instance && Battle.IsOnBoard(target))
        {
            return;
        }
        
        if (entity.owner == References.Player)
        {
            return;
        }

        if (BattleSaveSystem.instance.loading)
        {
            return;
        }
        
        ActionQueue.Stack(new ActionSequence(Run(GetTargets(new Hit(target, entity)))));
    }
}