using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectApplyXWhenItemUsed : StatusEffectApplyX
{
    public override void Init()
    {
        OnCardPlayed += Check;
    }

    public override bool RunCardPlayedEvent(Entity entity, Entity[] targets)
    {
        return entity.data.cardType.item;
    }
    
    private IEnumerator Check(Entity entity, Entity[] targets)
    {
        yield return Run(GetTargets(new Hit(entity, target), targets: targets));
    }

}