using System;
using System.Collections;
using System.Linq;
using PokemonMod.Patches;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectInstantEvolve : StatusEffectInstant
{
    public CardData evolveInto;
    public CardAnimation animation;

    public override IEnumerator Process()
    {
        yield return Evolve();
        
        yield return base.Process();
    }

    private IEnumerator Evolve()
    {
        var evolvedForm = evolveInto.Clone();

        var inventory = References.PlayerData.inventory;
        var deckCopy = inventory.deck.FirstOrDefault(deckCard => deckCard.id == target.data.id);
        foreach (var cardUpgradeData in target.data.upgrades)
        {
            if (cardUpgradeData.type == CardUpgradeData.Type.Crown || cardUpgradeData.CanAssign(evolvedForm))
            {
                cardUpgradeData.Clone().Assign(evolvedForm);
            }
            else if (deckCopy)
            {
                inventory.upgrades.Add(cardUpgradeData.Clone());
            }
        }
        
        var action = new ActionChangeForm(target, evolvedForm, animation)
        {
            priority = 10,
        };
        ActionQueue.Stack(action, true);

        if (deckCopy)
        {
            AdjustStats(deckCopy, evolvedForm);   
        }

        var cardIndex = inventory.deck.IndexOf(deckCopy);
        if (inventory.deck.RemoveWhere(deckCard => deckCard.id == target.data.id))
        {
            inventory.deck.Insert(cardIndex, evolvedForm);
        }
        
        CustomStats.AddPokemonEvolved();
        CardDiscoverSystem.instance.DiscoverCard(evolvedForm);
        Campaign.PromptSave();
        yield break;
    }

    private static void AdjustStats(CardData deckCopy, CardData evolvedCard)
    {
        var originalCard = Mod.GetCard(deckCopy.name);
        
        var healthDiff = deckCopy.hp - originalCard.hp;
        var damageDiff = deckCopy.damage - originalCard.damage;
        var counterDiff = deckCopy.counter - originalCard.counter;

        if (evolvedCard.hasHealth)
        {
            evolvedCard.hp = Math.Max(1, evolvedCard.hp + healthDiff);
        }

        if (evolvedCard.hasAttack)
        {
            evolvedCard.damage += damageDiff;
        }

        if (evolvedCard.counter > 0)
        {
            evolvedCard.counter = Math.Max(1, evolvedCard.counter + counterDiff);
        }
    }

    private class ActionChangeForm(Entity entity, CardData newForm, CardAnimation animation) : ActionChangePhase(entity, newForm, animation)
    {
        public override IEnumerator Run()
        {
            if (!entity.IsAliveAndExists())
            {
                yield break;
            }

            Events.InvokeEntityChangePhase(entity);

            PauseMenu.Block();
            DeckpackBlocker.Block();
            if (Deckpack.IsOpen && References.Player.entity.display is CharacterDisplay display)
            {
                display.CloseInventory();
            }

            if (animation)
            {
                yield return animation.Routine(entity);
            }

            foreach (var action in ActionQueue.GetActions())
            {
                switch (action)
                {
                    case ActionTrigger actionTrigger:
                        if (actionTrigger.entity == entity)
                        {
                            ActionQueue.Remove(action);
                        }

                        break;
                    case ActionEffectApply actionEffectApply:
                        actionEffectApply.TryRemoveEntity(entity);
                        break;
                }
            }

            var changeAction = new ActionSequence(Change(entity, newPhase))
            {
                note = "Evolve",
                priority = 10,
            };
            ActionQueue.Stack(changeAction, true);
            
            PauseMenu.Unblock();
            DeckpackBlocker.Unblock();
        }
        
        private new static IEnumerator Change(Entity entity, CardData newData)
        {
            entity.alive = false;
            yield return entity.ClearStatuses();
            entity.display.RemoveStatusIcon("damage", "damage");
            entity.display.RemoveStatusIcon("counter", "counter");
            entity.data = newData;
            yield return entity.display.UpdateData(true);
            entity.alive = true;
            yield return StatusEffectSystem.EntityEnableEvent(entity);
            
        }
    }
}