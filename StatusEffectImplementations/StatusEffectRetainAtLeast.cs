using System;

namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectRetainAtLeast : StatusEffectData
{
    public string retainType = "shroom";
    
    public override void Init()
    {
        Events.OnStatusEffectCountDown += CheckCountdown;
    }

    private void CheckCountdown(StatusEffectData status, ref int amount)
    {
        if (status.target != target || status.type != retainType)
        {
            return;
        }
        amount = Math.Min(amount, Math.Max(0, status.count - GetAmount()));
    }
}