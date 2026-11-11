using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectLoseHealthWhenPlayed : StatusEffectData
{
    public override void Init()
    {
        OnCardPlayed += DealDamage;
    }

    public override bool RunCardPlayedEvent(Entity entity, Entity[] targets)
    {
        return entity == target;
    }
    
    private IEnumerator DealDamage(Entity entity, Entity[] targets)
    {
        SfxSystem.OneShot("event:/sfx/status/shadeheart");
        
        target.lastHit = new Hit(target, target, GetAmount())
        {
            damageType = "summoned",
            countsAsHit = false,
            screenShake = 0.0f,
        };
        yield return target.lastHit.Process();
        yield return Sequences.Wait(0.1f);
    }
}