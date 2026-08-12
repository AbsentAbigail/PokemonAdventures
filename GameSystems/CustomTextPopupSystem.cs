using System;
using System.Collections;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Localization;

namespace PokemonMod.GameSystems;

[HarmonyPatch]
public class CustomTextPopupSystem : GameSystem
{
    private static CustomTextPopupSystem _instance;
    private static bool Exists() => (bool) _instance;
    
    private readonly Vector3 _textPopOffset = new(0.0f, 1.5f, -1f);
    private const float PopupDuration = 0.8f;

    public void OnEnable()
    {
        _instance = this;
    }

    public static IEnumerator RunWithShake(Entity entity, LocalizedString text, params object[] args)
    {
        if (!Exists())
        {
            yield break;
        }
        
        yield return _instance._RunWithShake(entity, text, args);
    }

    private IEnumerator _RunWithShake(Entity entity, LocalizedString text, params object[] args)
    {
        if (!enabled || !NoTargetTextSystem.Exists())
        {
            yield break;
        }

        yield return Sequences.WaitForAnimationEnd(entity);

        if (NoTargetTextSystem.instance is { } noTargetTextSystem)
        {
            var num1 = noTargetTextSystem.shakeDurationRange.Random();
            entity.curveAnimator.Move(noTargetTextSystem.shakeAmount.WithX(noTargetTextSystem.shakeAmount.x.WithRandomSign()), noTargetTextSystem.shakeCurve, duration: num1);
        }
        
        var textElement = NoTargetTextSystem.instance.textElement;
        textElement.text = text.GetLocalizedString().Format(args);
        PopText(entity.transform.position);
        yield return new WaitForSeconds(PopupDuration);
    }

    public static IEnumerator Run(Entity entity, LocalizedString text, params object[] args)
    {
        if (!Exists())
        {
            yield break;
        }
        
        yield return _instance._Run(entity, text, args);
    }

    private IEnumerator _Run(Entity entity, LocalizedString text, params object[] args)
    {
        if (!enabled || !NoTargetTextSystem.Exists())
        {
            yield break;
        }

        yield return Sequences.WaitForAnimationEnd(entity);
        var textElement = NoTargetTextSystem.instance.textElement;
        textElement.text = text.GetLocalizedString().Format(args);
        PopText(entity.transform.position);
        yield return new WaitForSeconds(PopupDuration);
    }

    public static IEnumerator RunNoWait(Entity entity, LocalizedString text, params object[] args)
    {
        if (!Exists())
        {
            yield break;
        }
        
        yield return _instance._RunNoWait(entity, text, args);
    }

    private IEnumerator _RunNoWait(Entity entity, LocalizedString text, params object[] args)
    {
        if (!enabled || !NoTargetTextSystem.Exists())
        {
            yield break;
        }

        var textElement = NoTargetTextSystem.instance.textElement;
        textElement.text = text.GetLocalizedString().Format(args);
        PopText(entity.transform.position);
    }

    public void PopText(Vector3 fromPos)
    {
        var textElement = NoTargetTextSystem.instance.textElement;
        var obj = textElement.gameObject;
        obj.SetActive(true);
        LeanTween.cancel(obj);
        obj.transform.position = fromPos;
        LeanTween.move(obj, fromPos + _textPopOffset, 1.5f).setEaseOutElastic();
        textElement.color = textElement.color.WithAlpha(1f);
        LeanTween.value(obj, 1f, 0.0f, 0.2f).setDelay(1.3f)
            .setOnUpdate(a => textElement.color = textElement.color.WithAlpha(a)).setOnComplete((Action) (() => obj.SetActive(false)));
    }
}