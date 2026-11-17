using System.Collections;
using PokemonMod.Helpers;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantConsumeRightmost : StatusEffectInstant
{
    public TargetConstraint[] constraints;
    
    public override IEnumerator Process()
    {
        var hand = References.Player.handContainer;

        LogHelper.Log("Start");
        var i = 0;
        foreach (var card in hand)
        {
            if (constraints.AllApply(card))
            {
                card.forceKill = DeathType.Consume;
                card.promptUpdate = true;
                card.Update();
                LogHelper.Log("Destroying card");
                i++;
            }
            
            if (i >= count)
            {
                
                LogHelper.Log("Break loop");
                break;
            }
        }
        
        
        LogHelper.Log("Done");
        yield return Remove();
    }
}