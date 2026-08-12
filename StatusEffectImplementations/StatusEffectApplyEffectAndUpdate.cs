using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantApplyEffectAndUpdate : StatusEffectInstantApplyEffect
{
    public override void Init()
    {
        OnBegin += Process;
    }

    private new IEnumerator Process()
    {
        yield return base.Process();
        target.display.promptUpdateDescription = true;
        target.PromptUpdate();
    }
}