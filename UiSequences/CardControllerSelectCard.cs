using UnityEngine;
using UnityEngine.Events;

namespace PokemonMod.UiSequences;

public class CardControllerSelectCardCardControllerSelectCard : CardController
{
  [Header("Press Tween")]
  public float cardPressScaleFrom = 0.8f;
  public float cardPressScaleTo = 1f;
  public LeanTweenType cardPressEase = LeanTweenType.easeOutElastic;
  public float cardPressEaseDur = 1f;
  public float cardPressWobble = 1f;
  public UnityEventEntity pressEvent;
  public UnityEventEntity hoverEvent;
  public UnityEventEntity unHoverEvent;

  public override bool AllowDynamicSelectRelease => false;

  public new void OnEnable()
  {
    Events.OnEntityHover += new UnityAction<Entity>(this.CardHover);
    Events.OnEntityUnHover += new UnityAction<Entity>(this.CardUnHover);
  }

  public new void OnDisable()
  {
    Events.OnEntityHover -= new UnityAction<Entity>(this.CardHover);
    Events.OnEntityUnHover -= new UnityAction<Entity>(this.CardUnHover);
  }

  public override void Press()
  {
    if (!this.canPress || !(bool) (Object) this.pressEntity || this.pressEntity.inPlay)
    {
      return;
    }
    Debug.Log((object) $"Pressing [{this.pressEntity.name}]");
    this.TweenHover(this.pressEntity);
    if ((double) this.cardPressEaseDur > 0.0)
    {
      LeanTween.scale(this.pressEntity.offset.gameObject, Vector3.one * this.cardPressScaleTo, this.cardPressEaseDur).setFrom(Vector3.one * this.cardPressScaleFrom).setEase(this.cardPressEase);
    }
    if ((double) this.cardPressWobble == 0.0)
    {
      return;
    }
    this.pressEntity.wobbler?.WobbleRandom(this.cardPressWobble);
  }

  public override void Release()
  {
    if (!(bool) (Object) this.pressEntity || !((Object) this.hoverEntity == (Object) this.pressEntity) || this.pressEntity.inPlay)
    {
      return;
    }
    Debug.Log((object) $"[{this}] PRESSING [{this.pressEntity}]! :D");
    var pressEntity = this.pressEntity;
    this.pressEntity = (Entity) null;
    this.pressEvent.Invoke(pressEntity);
  }

  public void CardHover(Entity entity) => this.hoverEvent.Invoke(entity);

  public void CardUnHover(Entity entity) => this.unHoverEvent.Invoke(entity);
}
