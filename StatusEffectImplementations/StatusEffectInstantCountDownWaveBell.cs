using System;
using System.Collections;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantCountDownWaveBell : StatusEffectInstant
{
    public override IEnumerator Process()
    {
        var waveBell = FindObjectOfType<WaveDeploySystemOverflow>(false);
        if (waveBell != null)
        {
            var localisedString = Mod.GetLocalizedString("CountDownWaveBell");
            FindObjectOfType<BattleLogSystem>()?.Log(localisedString, BattleLogType.Debuff, BattleLogSystem.GetBattleEntity(applier), GetAmount());

            var amount = Math.Min(GetAmount(), waveBell.counter);
            for (var i = 0; i < amount; i++)
            {
                var action = new ActionSequence(waveBell.CountDown())
                {
                    parallel = true,
                };
                ActionQueue.Add(action);
            }
        }

        yield return Remove();
    }
}