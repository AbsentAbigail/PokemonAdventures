using System.Linq;
using PokemonMod.StatusEffectImplementations;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonMod.Scriptables.ScriptableImages;

internal class TagTeamImage : ScriptableCardImage
{
    private Image Image => GetComponent<Image>();
    public Sprite sprite1;
    public Sprite sprite2;

    // gets called when the card is created (e.g. Leaders having one consistent avatar)
    public override void AssignEvent()
    {
        Image.sprite = entity.data.mainSprite;
    }

    public override void UpdateEvent()
    {
        var stanceChange = entity.statusEffects.FirstOrDefault(status => status is StatusEffectStanceChange) as StatusEffectStanceChange;
        if (!stanceChange)
        {
            return;
        }
        Image.sprite = stanceChange.isFirstStance ? sprite1 : sprite2;
    }
}