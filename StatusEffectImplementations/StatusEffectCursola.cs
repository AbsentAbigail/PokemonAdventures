using System.Collections;
using PokemonMod.GameSystems;

namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectCursola : StatusEffectData
{
    public CardData replaceInDeck;

    public override void Init()
    {
        OnEntityDestroyed += OnSelfKill;
    }

    public override bool RunEntityDestroyedEvent(Entity entity, DeathType deathType)
    {
        return entity == target && entity.lastHit?.attacker == target;
    }

    private IEnumerator OnSelfKill(Entity entity, DeathType deathType)
    {
        var deck = References.PlayerData.inventory.deck;

        if (!deck.Remove(target.data))
        {
            yield break;
        }
        
        target.data.upgrades.ForEach(upgrade =>
        {
            References.PlayerData.inventory.upgrades.Add(upgrade.Clone());
        });
        deck.Add(replaceInDeck.Clone());
        CardDiscoverSystem.instance.DiscoverCard(replaceInDeck);
        yield return CustomTextPopupSystem.Run(target, Mod.GetLocalizedString("CorsolaDies"));
    }
}