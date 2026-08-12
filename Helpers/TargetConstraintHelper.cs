using System;
using System.Linq;
using JetBrains.Annotations;
using PokemonMod.Scriptables.TargetConstraints;

namespace PokemonMod.Helpers;

[PublicAPI]
public class TargetConstraintHelper
{
    public static T General<T>(string name = null, Action<T> modification = null, bool not = false)
        where T : TargetConstraint, new()
    {
        T targetConstraint = new Script<T>(name ?? typeof(T).Name, modification);
        targetConstraint.not = not;
        return targetConstraint;
    }

    public static TargetConstraintOr IsOfTypes(string[] types, bool not = false)
    {
        var constraints = types.Select(type => HasStatus(type));
        return Or((not ? "Does Not Have" : "Has") + $" type matching {string.Join(", ", types)}", not, constraints.ToArray<TargetConstraint>());
    }

    public static TargetConstraintHasStatus HasStatus(string status,
        Action<TargetConstraintHasStatus> modification = null, bool not = false)
    {
        var targetConstraint = General((not ? "Does Not Have" : "Has") + $" Status {status}", modification, not);
        targetConstraint.status = Mod.GetStatus(status);
        return targetConstraint;
    }

    public static TargetConstraintWeather Weather(string weather,
        Action<TargetConstraintWeather> modification = null, bool not = false)
    {
        var targetConstraint = General((not ? "Weather not active" : "Weather active") + $" {weather}", modification, not);
        targetConstraint.weather = Mod.GetCard(weather);
        return targetConstraint;
    }

    public static TargetConstraintHasTrait HasTrait(string trait, Action<TargetConstraintHasTrait> modification = null,
        bool not = false)
    {
        var targetConstraint = General((not ? "Does Not Have" : "Has") + $" Trait {trait}", modification, not);
        targetConstraint.trait = Mod.GetTrait(trait);
        return targetConstraint;
    }

    public static TargetConstraintAnd ApplyCharmConstraint(Action<TargetConstraintAnd> modification = null,
        bool not = false)
    {
        var targetConstraint = And("Does Trigger, Plays On Board, Does Not Play On Slot", not,
            DoesTrigger(),
            General<TargetConstraintPlayOnSlot>("Plays On Board", tc => tc.board = true),
            General<TargetConstraintPlayOnSlot>("Does Not Play On Slot", tc => tc.slot = true, true)
        );
        modification?.Invoke(targetConstraint);
        return targetConstraint;
    }

    public static TargetConstraintOr DoesTrigger(Action<TargetConstraintOr> modification = null, bool not = false)
    {
        var targetConstraint = Or(not ? "Does Not Trigger" : "Does Trigger", not,
            MaxCounterMoreThan(0),
            General<TargetConstraintHasReaction>("Has Reaction"),
            General<TargetConstraintIsItem>("Is Item")
        );
        modification?.Invoke(targetConstraint);
        return targetConstraint;
    }

    public static TargetConstraintOr HasCounterOrReaction(Action<TargetConstraintOr> modification = null,
        bool not = false)
    {
        var targetConstraint = Or(not ? "Does Not Have Counter Or Reaction" : "Has Counter Or Reaction", not,
            MaxCounterMoreThan(0),
            General<TargetConstraintHasReaction>("Has Reaction")
        );
        modification?.Invoke(targetConstraint);
        return targetConstraint;
    }

    public static TargetConstraintAttackMoreThan AttackMoreThan(int moreThan,
        Action<TargetConstraintAttackMoreThan> modification = null, bool not = false)
    {
        var targetConstraint =
            General((not ? "Does Not Have" : "Has") + $" Attack More Than {moreThan}", modification, not);
        targetConstraint.value = moreThan;
        return targetConstraint;
    }

    public static TargetConstraintHealthMoreThan HealthMoreThan(int moreThan,
        Action<TargetConstraintHealthMoreThan> modification = null, bool not = false)
    {
        var targetConstraint =
            General((not ? "Does Not Have" : "Has") + $" Health More Than {moreThan}", modification, not);
        targetConstraint.value = moreThan;
        return targetConstraint;
    }

    public static TargetConstraintMaxCounterMoreThan MaxCounterMoreThan(int moreThan,
        Action<TargetConstraintMaxCounterMoreThan> modification = null, bool not = false)
    {
        var targetConstraint = General((not ? "Does Not Have" : "Has") + $" Max Counter More Than {moreThan}",
            modification, not);
        targetConstraint.moreThan = moreThan;
        return targetConstraint;
    }

    public static TargetConstraintHasAttackEffect HasAttackEffect(string status,
        Action<TargetConstraintHasAttackEffect> modification = null, bool not = false)
    {
        var targetConstraint = General((not ? "Does Not Have" : "Has") + $" Attack Effect {status}", modification, not);
        targetConstraint.effect = Mod.GetStatus(status);
        return targetConstraint;
    }

    public static TargetConstraintHasEffectBasedOn HasEffectBasedOn(string status,
        Action<TargetConstraintHasEffectBasedOn> modification = null, bool not = false)
    {
        var targetConstraint =
            General((not ? "Does Not Have" : "Has") + $" Effect Based On {status}", modification, not);
        var type = Mod.GetStatus(status).type;
        targetConstraint.basedOnStatusType = type;
        return targetConstraint;
    }

    public static TargetConstraintIsCardType IsCardType(string[] cardTypes,
        Action<TargetConstraintIsCardType> modification = null, bool not = false)
    {
        var targetConstraint = General((not ? "Is of" : "Is not of") + $" card types [{cardTypes}]", modification, not);
        targetConstraint.allowedTypes = cardTypes.Select(Mod.GetCardType).ToArray();
        return targetConstraint;
    }

    public static TargetConstraintOr Or(string name, bool not = false, params TargetConstraint[] constraints)
    {
        var targetConstraint = General<TargetConstraintOr>(name, not: not);
        targetConstraint.constraints = constraints;
        return targetConstraint;
    }

    public static TargetConstraintAnd And(string name, bool not = false, params TargetConstraint[] constraints)
    {
        var targetConstraint = General<TargetConstraintAnd>(name, not: not);
        targetConstraint.constraints = constraints;
        return targetConstraint;
    }
}