using System.Collections;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectDealAdditionalDamage : StatusEffectData
{
    public ScriptableAmount scriptableAmount;
    public TargetConstraint[] attackerConstraints;
    private int _currentAmount;
    private bool _toReset;

    public override void Init()
    {
        PreCardPlayed += Gain;
    }

    public override IEnumerator RemoveStacks(int amount, bool removeTemporary)
    {
        count -= amount;
        if (removeTemporary)
        {
            temporary -= amount;
        }
        if (count <= 0)
        {
            LoseCurrentAmount();
            yield return Remove();
        }
        target.PromptUpdate();
    }

    public override bool RunPreCardPlayedEvent(Entity entity, Entity[] targets)
    {
        return entity == target && CanTrigger() && attackerConstraints.All(constraint => constraint.Check(entity));
    }

    private IEnumerator Gain(Entity entity, Entity[] targets)
    {
        var amount = scriptableAmount ? scriptableAmount.Get(target) : GetAmount();
        switch (_toReset)
        {
            case true when amount == _currentAmount: 
                yield break;
            case true:
                LoseCurrentAmount(); 
                break;
        }

        if (amount > 0)
        {
          yield return GainAmount(amount);
        }
    }

    private IEnumerator GainAmount(int amount)
    {
        _toReset = true;
        var num1 = target.tempDamage.Value;
        target.tempDamage += amount;
        _currentAmount = target.tempDamage.Value - num1;
        target.PromptUpdate();
        target.curveAnimator.Ping();
        yield return Sequences.Wait(0.5f);
    }

    public override bool RunTurnEndEvent(Entity entity)
    {
        if (entity == target.owner.entity && _toReset)
        {
            LoseCurrentAmount();
        }
        return false;
    }

    public void LoseCurrentAmount()
    {
        _toReset = false;
        if (_currentAmount == 0)
        {
            return;
        }
        target.tempDamage -= _currentAmount;
        _currentAmount = 0;
        target.PromptUpdate();
    }
}
