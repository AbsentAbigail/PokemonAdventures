using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantTransferStatus : StatusEffectInstant
{
    public string transferType = "scrap";
    
    public override IEnumerator Process()
    {
        var status = target.FindStatus(transferType);

        if (status == null)
        {
            yield return base.Process();
            yield break;
        }

        yield return StatusEffectSystem.Apply(applier, applier, status, status.count);
        yield return status.Remove();
        yield return base.Process();
    }
}