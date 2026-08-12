using System.Collections;
using PokemonMod.GameSystems;
using Random = UnityEngine.Random;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectParalysis : StatusEffectData
{
    public override void Init()
    {
        Events.OnEntityCountDown += Paralysis;
        OnTurnEnd += Reduce;
    }

    public override bool RunTurnEndEvent(Entity entity)
    { 
        return entity == target;
    }

    private IEnumerator Reduce(Entity entity)
    {
        var amount = 1;
        Events.InvokeStatusEffectCountDown(this, ref amount);
        yield return CountDown(target, amount);
    }

    private void OnDestroy()
    {
        Events.OnEntityCountDown -= Paralysis;
    }

    private void Paralysis(Entity entity, ref int countdownAmount)
    {
        if (entity != target)
        {
            return;
        }

        if (Random.Range(0, 2) != 0)
        {
            return;
        }

        ActionQueue.Add(new ActionSequence(CustomTextPopupSystem.RunWithShake(target, Mod.GetLocalizedString("Paralysed"), target.data.title)));
        countdownAmount = 0;
    }
}