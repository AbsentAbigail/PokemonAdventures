using System.Collections.Generic;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectReduceSuperEffectiveDamage : StatusEffectData
{
    private static readonly HashSet<Hit> Hits = [];

    public override void Init()
    {
        StatusEffectType.OnSuperEffectiveEvent += Check;
    }

    private void OnDestroy()
    {
        StatusEffectType.OnSuperEffectiveEvent -= Check;
    }

    public override bool RunTurnEndEvent(Entity entity)
    {
        if (Hits.Any())
        {
            Hits.Clear();
        }
        
        return false;
    }

    private void Check(Hit hit, string attackingType, string defendingType)
    {
        if (!Hits.Add(hit))
        {
            return;
        }
        var amount = GetAmount();
        hit.damage -= amount;
        
        var localisedString = Mod.GetLocalizedString("KasibLog");
        FindObjectOfType<BattleLogSystem>()?.Log(localisedString, BattleLogType.Buff, BattleLogSystem.GetBattleEntity(applier), amount);
    }
}