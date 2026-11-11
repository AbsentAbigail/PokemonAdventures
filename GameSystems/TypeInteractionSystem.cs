using System.Linq;
using PokemonMod.StatusEffectImplementations;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace PokemonMod.GameSystems;

public class TypeInteractionSystem : GameSystem
{
    public static event UnityAction<Hit> OnSuperEffectiveEvent;
    public static void InvokeSuperEffectiveEvent(Hit hit) => OnSuperEffectiveEvent?.Invoke(hit);

    public static event UnityAction<Hit> OnResistEvent;
    public static void InvokeResistEvent(Hit hit) => OnResistEvent?.Invoke(hit);

    public static event UnityAction<Hit> OnImmuneEvent;
    public static void InvokeImmuneEvent(Hit hit) => OnImmuneEvent?.Invoke(hit);

    private static LocalizedString _superEffectiveText;
    private static LocalizedString _resistText;
    private static LocalizedString _immuneText;

    public void OnEnable()
    {
        _superEffectiveText = Mod.GetLocalizedString("SuperEffectiveLog");
        _resistText = Mod.GetLocalizedString("ResistedLog");
        _immuneText = Mod.GetLocalizedString("ImmuneLog");
        
        Events.OnEntityHit += Check;
    }
    
    public void OnDisable()
    {
        Events.OnEntityHit -= Check;
    }

    private static void Check(Hit hit)
    {
        if (!(hit.Offensive && hit.BasicHit && hit.canRetaliate))
        {
            return;
        }
        
        var attacker = hit.attacker;
        var enemy = hit.target;

        if (!attacker)
        {
            return;
        }
        if (!enemy)
        {
            return;
        }
        
        var isSupereffective = false;
        var isResisted = false;
        var isImmune = false;
        var damageModifier = 0;
        
        foreach (var statusType in attacker.statusEffects.Where(status => status is StatusEffectType))
        {
            var attackingType = (StatusEffectType)statusType;
            foreach (var defendingType in enemy.statusEffects.Where(status => attackingType.weakTypes.Contains(status.type)))
            {
                isSupereffective = true;
                var modifier = attackingType.weaknessModifier;
                FindObjectOfType<BattleLogSystem>()?.Log(_superEffectiveText, BattleLogType.Buff, attackingType.type, defendingType.type, modifier);
                damageModifier += modifier;
            }
            foreach (var defendingType in enemy.statusEffects.Where(status => attackingType.resistingTypes.Contains(status.type)))
            {
                isResisted = true;
                var modifier = attackingType.resistanceModifier;
                FindObjectOfType<BattleLogSystem>()?.Log(_resistText, BattleLogType.Buff, attackingType.type, defendingType.type, modifier);
                damageModifier += modifier;
            }
            foreach (var defendingType in enemy.statusEffects.Where(status => attackingType.immuneTypes.Contains(status.type)))
            {
                isImmune = true;
                var modifier = attackingType.immunityModifier;
                FindObjectOfType<BattleLogSystem>()?.Log(_immuneText, BattleLogType.Buff, attackingType.type, defendingType.type, modifier);
                damageModifier += modifier;
            }
        }

        hit.damage += damageModifier;
        
        if (isSupereffective)
        {
            InvokeSuperEffectiveEvent(hit);
        }
        if (isResisted)
        {
            InvokeResistEvent(hit);
        }
        if (isImmune)
        {
            InvokeImmuneEvent(hit);
        }
    }
}