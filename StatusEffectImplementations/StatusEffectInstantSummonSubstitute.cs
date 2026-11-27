using System.Collections;
using System.Linq;
using UnityEngine;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantSummonSubstitute : StatusEffectInstantSummon
{
    public StatusEffectData substituteEffect;

    private static CardContainer ReserveContainer => References.Player.reserveContainer;

    private static bool _inUse;

    public override IEnumerator Process()
    {
        if (!target.alive)
        {
            var notEnoughHealthLocalisedString = Mod.GetLocalizedString("NotEnoughHealthSubstitute");
            FindObjectOfType<BattleLogSystem>()?.Log(notEnoughHealthLocalisedString, BattleLogType.Consume, BattleLogSystem.GetBattleEntity(target));
            yield break;
        }

        while (_inUse)
        {
            yield return null;
        }
        _inUse = true;

        var summonLocalisedString = Mod.GetLocalizedString("SummonSubstitute");
        FindObjectOfType<BattleLogSystem>()?.Log(summonLocalisedString, BattleLogType.Buff, BattleLogSystem.GetBattleEntity(target));

        var originalData = targetSummon.summonCard;
        targetSummon.summonCard = CopyDataToSummon();
        var summonTo = target.containers[0];

        var controller = target.display.hover.controller;
        target.curveAnimator.Ping();

        yield return MoveToReserveSequence();
        yield return TrySummon(summonTo, controller, target);

        targetSummon.summonCard = originalData;
        _inUse = false;
        yield return Remove();
    }

    private CardData CopyDataToSummon()
    {
        var cardData = targetSummon.summonCard.Clone();

        cardData.hasAttack = target.data.hasAttack;
        cardData.damage = target.damage.current;
        cardData.counter = target.counter.max;

        cardData.startWithEffects =
        [
            .. CardData.StatusEffectStacks.Stack(
                cardData.startWithEffects,
                target.statusEffects
                    .Where(status => target.traits.All(trait => trait.data.effects.All(traitEffect => traitEffect.name != status.name)))
                    .Where(status => status is not StatusEffectInstant)
                    .Select(status => new CardData.StatusEffectStacks(status, status.count))
            ),
            new CardData.StatusEffectStacks(substituteEffect, 1),
        ];
        cardData.attackEffects =
        [
            .. target.attackEffects,
        ];
        cardData.traits =
        [
            .. target.traits.Select(trait => new CardData.TraitStacks(trait.data, trait.count)),
        ];

        cardData.SetCustomData("substitute", target.data.id);
        return cardData;
    }

    private IEnumerator MoveToReserveSequence()
    {
        target.flipper.FlipDown();
        const float animationTime = 0.4f;
        target.RemoveFromContainers();
        ReserveContainer.Add(target);
        LeanTween.scale(target.gameObject, Vector2.zero, animationTime).setEaseInBack();
        yield return Sequences.Wait(animationTime);
        ReserveContainer.SetChildPosition(target);
        foreach (var container in target.preContainers)
        {
            container.TweenChildPositions();
        }
    }

    private IEnumerator TrySummon(
        CardContainer container,
        CardController controller,
        Entity summonedBy)
    {
        if (container.Count < container.max)
        {
            yield return targetSummon.Summon(container, controller, summonedBy);
        }
        else
        {
            if (ShoveSystem.CanShove(container.GetTop(), target, out var shoveData))
            {
                yield return ShoveSystem.DoShove(shoveData, true);
                yield return targetSummon.Summon(container, controller, summonedBy);
            }
            else if (NoTargetTextSystem.Exists())
            {
                yield return NoTargetTextSystem.Run(target, NoTargetType.NoSpaceToSummon);
            }
        }
    }
}