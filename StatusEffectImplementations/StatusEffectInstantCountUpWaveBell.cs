using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantCountUpWaveBell : StatusEffectInstant
{
    public override IEnumerator Process()
    {
        var waveBell = FindObjectOfType<WaveDeploySystemOverflow>(false);
        if (waveBell != null)
        {
            var localisedString = Mod.GetLocalizedString("CountUpWaveBell");
            FindObjectOfType<BattleLogSystem>()?.Log(localisedString, BattleLogType.Buff, BattleLogSystem.GetBattleEntity(applier), GetAmount());
            
            SfxSystem.OneShot("event:/sfx/inventory/wave_counter_refresh");
            
            waveBell.SetCounter(waveBell.counter + GetAmount());
        }

        yield return Remove();
    }
}