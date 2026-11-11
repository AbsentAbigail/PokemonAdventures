using System.Collections;
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
        PostApplyStatus += ChangeIcon;
        OnHit += Hit;
        OnStack += Stack;
    }

    private IEnumerator Stack(int stacks)
    {
        yield return Run([target], stacks);
    }

    private IEnumerator Hit(Hit hit)
    {
        if (hit.damageType != TargetType)
        {
            yield break;
        }

        var status = target.FindStatus(TargetType);
        if (!status)
        {
            yield break;
        }
        
        yield return StatusEffectSystem.Apply(target, target, status, hit.damage + hit.damageBlocked);
    }

    public override bool RunPostApplyStatusEvent(StatusEffectApply apply)
    {
        if (target != null && apply?.target == target && GetAmount() > 0)
        {
            return apply!.effectData.type == TargetType;
        }
        return false;
    }

    private IEnumerator ChangeIcon(StatusEffectApply apply)
    {
        var shroomIcon = apply.target.GetComponent<Card>().FindStatusIcon(TargetType);
        if (shroomIcon != null && sprite != null)
        {
            shroomIcon.GetComponent<Image>().sprite = sprite;
            shroomIcon.transform.SetParent(shroomIcon.transform.parent.parent.Find("HealthLayout"));
        }
        else
        {
            shroomIcon = apply.target.GetComponent<Card>().SetStatusIcon(TargetType, "health", new Stat(apply.count, 0));
            shroomIcon.GetComponent<Image>().sprite = sprite;
        }
        var popup = shroomIcon.GetComponent<CardPopUpTarget>();
        popup.keywords =
        [
            .. popup.keywords,
            keywordData,
        ];
        
        yield return Sequences.Wait(apply.target.curveAnimator.Ping());
    }
}