using System.Collections;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectRecycleItem : StatusEffectData
{
    public StatusEffectSummon summonEffect;
    public TargetConstraint[] cardConstraints;
    public StatusEffectData[] withEffects;
    
    public override void Init()
    {
        OnEntityDestroyed += Recycle;
    }

    public override bool RunEntityDestroyedEvent(Entity entity, DeathType deathType)
    {
        if (entity == target || !entity.data.cardType.item || deathType != DeathType.Consume)
        {
            return false;
        }

        return cardConstraints.All(constraint => constraint.Check(entity));
    }
    
    private IEnumerator Recycle(Entity entity, DeathType deathType)
    {
        target.curveAnimator.Ping();
        yield return Sequences.Wait(0.13f);
        summonEffect.summonCard = entity.data.Clone();
        yield return summonEffect.Summon(References.Player.discardContainer, target.display.hover.controller, target, withEffects, GetAmount());
    }
}