using System.Collections;
using PokemonMod.GameSystems;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectBurn : StatusEffectData
{
    public override void Init()
    {
        OnTurnEnd += DoubleOverburn;
        OnStack += Stack;
    }

    private IEnumerator Stack(int stacks)
    {
        target.tempDamage -= stacks;
        yield break;
    }

    public override IEnumerator RemoveStacks(int amount, bool removeTemporary)
    {
        target.tempDamage += amount;
        yield return base.RemoveStacks(amount, removeTemporary);
    }

    public override bool RunTurnEndEvent(Entity entity)
    {
        return target.enabled && entity == target && target.FindStatus("overload");
    }

    private IEnumerator DoubleOverburn(Entity entity)
    {
        var overburn = target.FindStatus("overload");
        if (!overburn)
        {
            yield break;
        }
        yield return StatusEffectSystem.Apply(target, applier, overburn, overburn.GetAmount());
        
        var amount = 1;
        Events.InvokeStatusEffectCountDown(this, ref amount);
        if (amount != 0)
        {
            yield return CountDown(target, amount);
        }
    }
}