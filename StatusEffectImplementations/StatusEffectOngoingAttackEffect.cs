using System.Collections;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectOngoingAttackEffect : StatusEffectOngoing
{
    public StatusEffectData attackEffect;
    
    public override IEnumerator Add(int add)
    {
        Change(add);
        target.display.promptUpdateDescription = true;
        target.PromptUpdate();
        yield break;
    }

    public override IEnumerator Remove(int remove)
    {
        Change(-remove);
        target.display.promptUpdateDescription = true;
        target.PromptUpdate();
        yield break;
    }

    private void Change(int change)
    {
        var previous = target.attackEffects.FirstOrDefault(a => a.data.name == attackEffect.name);

        if (previous != null)
        {
            previous.count += change;
            
            if (previous.count <= 0)
            {
                target.attackEffects.Remove(previous);
            }

            return;
        }

        if (change <= 0)
        {
            return;
        }
        var effectStack = new CardData.StatusEffectStacks(attackEffect, change);
        target.attackEffects.Add(effectStack);
    }
}