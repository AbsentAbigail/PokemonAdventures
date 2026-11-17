using System;
using System.Collections;
using System.Collections.Generic;
using HarmonyLib;
using PokemonMod.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using Object = UnityEngine.Object;

namespace PokemonMod.GameSystems;

[HarmonyPatch]
public class TypePreviewSystem : GameSystem
{
    private static TypePreviewSystem _instance;
    private static bool Exists() => (bool) _instance;

    private TargetConstraint _isNotPokemon;
    private List<TMP_Text> textElements = [];
    
    private readonly Vector3 _textPopOffset = new(0.0f, 1.5f, -1f);
    

    public void OnEnable()
    {
        _instance = this;
        Events.OnEntityHover += Run;
        _isNotPokemon = TargetConstraintHelper.IsPokemon(not: true);
    }

    private void OnDestroy()
    {
        Events.OnEntityHover -= Run;
    }

    public static void Run(Entity entity)
    {
        _instance?._Run(entity);
    }
    
    public void _Run(Entity entity)
    {
        if (!enabled)
        {
            return;
        }

        foreach (var textElement in textElements)
        {
            textElement.Destroy();
        }

        if (!Battle.instance || !Battle.IsOnBoard(entity))
        {
            return;
        }
        
        if (_isNotPokemon.Check(entity))
        {
            return;
        }

        foreach (var enemy in entity.GetAllEnemies())
        {
            if (_isNotPokemon.Check(enemy))
            {
                continue;
            }
            
            var fromPos = enemy.transform.position;
            
            var textElement = Instantiate(NoTargetTextSystem.instance.textElement);

            var damageModifier = TypeInteractionSystem.GetDamagePreview(entity, enemy);
            textElement.text = (damageModifier > 0 ? "Super effective!\n+" 
                : (damageModifier == 0 ? "+"
                : "Not very effective!\n"))
                               + $"{damageModifier}";
            
            var textObject = textElement.gameObject;
            
            textObject.SetActive(true);
            LeanTween.cancel(textObject);
            textObject.transform.position = fromPos;
            textObject.transform.localScale *= 0.75f;
            LeanTween.move(textObject, fromPos + _textPopOffset, 1.5f).setEaseOutElastic();
            textElement.color = textElement.color.WithAlpha(1f);
            LeanTween.value(textObject, 1f, 0.0f, 1f)
                .setDelay(1.3f)
                .setOnUpdate(a => textElement.color = textElement.color.WithAlpha(a))
                .setOnComplete((Action) (() =>
                {
                    textObject.SetActive(false);
                    textObject.Destroy();
                }));
            
            textElements.Add(textElement);
        }
    }
}