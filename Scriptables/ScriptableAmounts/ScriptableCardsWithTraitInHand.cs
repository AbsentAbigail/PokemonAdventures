using System.Linq;

namespace PokemonMod.Scriptables.ScriptableAmounts;

internal class ScriptableCardsWithTraitInHand : ScriptableAmount
{
    public TraitData traitData;

    public override int Get(Entity entity)
    {
        return References.Player.handContainer.Count(card => card.traits.Any(trait => trait.data.name == traitData.name));
    }
}