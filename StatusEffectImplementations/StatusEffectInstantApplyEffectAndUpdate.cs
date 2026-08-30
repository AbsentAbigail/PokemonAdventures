using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantApplyEffectAndUpdate : StatusEffectInstantApplyEffect
{
    public override IEnumerator Process()
    {
        yield return base.Process();
        target.display.promptUpdateDescription = true;
        target.PromptUpdate();
    }
}