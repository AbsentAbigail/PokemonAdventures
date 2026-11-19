using System.Collections;
using PokemonMod.Builders.Keywords;
using PokemonMod.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectBadlyShroomed : StatusEffectApplyX
{
    public Sprite sprite;
    public KeywordData keywordData;
    private const string TargetType = "shroom";

    public override void Init()
    {
        OnApplyStatus += CheckCleanse;
        PostApplyStatus += ChangeIcon;
    }

    public override bool RunPostHitEvent(Hit hit)
    {
        if (hit.target != target)
        {
            return false;
        }

        if (hit.damageType == TargetType)
        {
            ActionQueue.Add(new ActionSequence(
                StatusEffectSystem.Apply(target, hit.attacker, effectToApply, hit.damage + hit.damageBlocked)));
        }

        return false;
    }

    public override bool RunApplyStatusEvent(StatusEffectApply apply)
    {
        return apply.effectData is StatusEffectInstantCleanse;
    }

    private IEnumerator CheckCleanse(StatusEffectApply apply)
    {
        yield return Remove();
    }

    // Update icon when first applied
    public override bool RunStackEvent(int stacks)
    {
        ChangeIcon(stacks);

        return base.RunStackEvent(stacks);
    }

    public override bool RunPostApplyStatusEvent(StatusEffectApply apply)
    {
        if (apply?.target == target && count > 0)
        {
            return apply!.effectData.type == TargetType;
        }
        return false;
    }

    private IEnumerator ChangeIcon(StatusEffectApply apply)
    {
        LogHelper.Log("Post apply: " + apply.effectData.type);

        ChangeIcon(apply.count);

        yield return Sequences.Wait(apply.target.curveAnimator.Ping());
    }

    private void ChangeIcon(int amount)
    {
        var shroomIcon = target.GetComponent<Card>().FindStatusIcon(TargetType);
        if (shroomIcon != null && sprite != null)
        {
            shroomIcon.GetComponent<Image>().sprite = sprite;
            shroomIcon.transform.SetParent(shroomIcon.transform.parent.parent.Find("HealthLayout"));
        }
        else
        {
            shroomIcon = target.GetComponent<Card>().SetStatusIcon(TargetType, "health", new Stat(amount, 0));
            shroomIcon.GetComponent<Image>().sprite = sprite;
        }
        var popup = shroomIcon.GetComponent<CardPopUpTarget>();
        popup.keywords =
        [
            .. popup.keywords,
            keywordData,
        ];

        shroomIcon.textElement.enabled = true;
        shroomIcon.textElement.color = KeywordColours.CatColor;
        shroomIcon.textColour = KeywordColours.CatColor;
    }
}