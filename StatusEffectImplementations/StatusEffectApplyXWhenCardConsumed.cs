namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectApplyXWhenCardConsumed : StatusEffectApplyXWhenCardDestroyed
{
    public DeathType requiredDeathType = DeathType.Consume;
    
    public override bool RunEntityDestroyedEvent(Entity entity, DeathType deathType)
    {
        return deathType == requiredDeathType && base.RunEntityDestroyedEvent(entity, deathType);
    }
}