using System.Linq;

namespace PokemonMod.Scriptables.ScriptableAmounts;

internal class ScriptableCardTypeInHand : ScriptableAmount
{
    public string type;

    public override int Get(Entity entity)
    {
        return References.Player.handContainer.Count(card => card.data.cardType.name == type);
    }
}