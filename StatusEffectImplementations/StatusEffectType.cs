using System.Collections;
using System.Linq;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectType : StatusEffectData
{
    public int weakness = 2;
    public int resistance = -2;
    public int immunity = -4;
    
    public string[] weakTypes;
    public string[] resistingTypes;
    public string[] immuneTypes;

    public override void Init()
    {
        OnHit += Check;
    }

    public override bool RunHitEvent(Hit hit)
    {
        return hit.attacker == target && hit.Offensive;
    }

    private IEnumerator Check(Hit hit)
    {
        var enemy = hit.target;
        Check(enemy, hit, weakTypes, weakness, "SuperEffectiveLog");
        Check(enemy, hit, resistingTypes, resistance, "ResistedLog");
        Check(enemy, hit, immuneTypes, immunity, "ImmuneLog");
        yield break;
    }

    private void Check(Entity enemy, Hit hit, string[] types, int modifier, string battleLogKey)
    {
        var localisedString = Mod.GetLocalizedString(battleLogKey);
        
        foreach (var effect in enemy.statusEffects.Where(effect => types.Contains(effect.type)))
        {
            FindObjectOfType<BattleLogSystem>()?.Log(localisedString, BattleLogType.Buff, type, effect.type, modifier);
            hit.damage += modifier;
        }
    }
}