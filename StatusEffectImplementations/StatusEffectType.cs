using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectType : StatusEffectData
{
    public static event UnityAction<Hit, string, string> OnSuperEffectiveEvent;
    public static void InvokeSuperEffectiveEvent(Hit hit, string attackingType, string defendingType) => OnSuperEffectiveEvent?.Invoke(hit, attackingType, defendingType);

    public static event UnityAction<Hit, string, string> OnResistEvent;
    public static void InvokeResistEvent(Hit hit, string attackingType, string defendingType) => OnResistEvent?.Invoke(hit, attackingType, defendingType);

    public static event UnityAction<Hit, string, string> OnImmuneEvent;
    public static void InvokeImmuneEvent(Hit hit, string attackingType, string defendingType) => OnImmuneEvent?.Invoke(hit, attackingType, defendingType);

    public int weakness = 2;
    public int resistance = -2;
    public int immunity = -4;
    
    public string[] weakTypes;
    public string[] resistingTypes;
    public string[] immuneTypes;

    public StatusEffectData kasibBerry;

    private static readonly Dictionary<string, string> TypeMatching = [];

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
        foreach (var interaction in Check(hit, weakTypes, weakness, "SuperEffectiveLog"))
        {
            InvokeSuperEffectiveEvent(hit, interaction.Item1, interaction.Item2);
        }
        foreach (var interaction in Check(hit, resistingTypes, resistance, "ResistedLog"))
        {
            InvokeResistEvent(hit, interaction.Item1, interaction.Item2);
        }
        foreach (var interaction in Check(hit, immuneTypes, immunity, "ImmuneLog"))
        {
            InvokeImmuneEvent(hit, interaction.Item1, interaction.Item2);
        }
        yield break;
    }

    private List<(string, string)> Check(Hit hit, string[] types, int modifier, string battleLogKey)
    {
        List<(string, string)> result = [];
        var enemy = hit.target;
        var localisedString = Mod.GetLocalizedString(battleLogKey);
        
        foreach (var effect in enemy.statusEffects.Where(effect => CheckType(effect.type, types)))
        {
            FindObjectOfType<BattleLogSystem>()?.Log(localisedString, BattleLogType.Buff, type, effect.type, modifier);
            hit.damage += modifier;
            result.Add((type, effect.type));
        }
        return result;
    }
    
    private static bool CheckType(string typeName, string[] types)
    {
        foreach (var type in types)
        {
            if (!TypeMatching.ContainsKey(type))
            {
                TypeMatching[type] = Mod.GetStatus(type).type;
            }
            if (TypeMatching[type] == typeName)
            {
                return true;
            }
        }
        
        return false;
    }
}