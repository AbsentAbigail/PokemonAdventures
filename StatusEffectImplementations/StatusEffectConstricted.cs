using System.Collections;
using PokemonMod.GameSystems;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectConstricted : StatusEffectApplyX
{
    public override void Init()
    {
        OnCardMove += CheckRecall;
        OnEntityDestroyed += CheckDestroy;
        OnTurnEnd += Damage;
    }

    public override bool RunTurnEndEvent(Entity entity)
    {
        if (entity != target)
        {
            return false;
        }
        
        if (Battle.IsOnBoard(applier))
        {
            return target.enabled && entity == target;
        }
        
        ActionQueue.Stack(new ActionSequence(Freed()));
        return false;
    }

    private IEnumerator Damage(Entity entity)
    {
        return new Hit(GetDamager(), target, count)
        {
            screenShake = 0.25f,
            damageType = type,
        }.Process();
    }

    public override bool RunCardMoveEvent(Entity entity)
    {
        return entity == applier && entity.containers.Length > 0 && entity.containers[0] == References.Player.discardContainer;
    }

    private IEnumerator CheckRecall(Entity entity)
    {
        return Freed();
    }

    public override bool RunEntityDestroyedEvent(Entity entity, DeathType _)
    {
        return entity == applier;
    }

    private IEnumerator CheckDestroy(Entity _, DeathType __)
    {
        return Freed();
    }

    private IEnumerator Freed()
    {
        yield return CustomTextPopupSystem.RunWithShake(target, Mod.GetLocalizedString("ConstrictedFreed"), target.data.title);
        yield return Remove();
    }
}