using System.Linq;

namespace PokemonMod.Scriptables.ScriptableAmounts;

internal class ScriptableWeatherIntensity : ScriptableAmount
{
    public CardData weatherCard;
    public StatusEffectData intensityEffect;

    public override int Get(Entity entity)
    {
        var weather = Battle.GetCardsOnBoard().FirstOrDefault(ally => ally.data.name == weatherCard.name);

        if (!weather)
        {
            return 0;
        }
        
        return weather.statusEffects.FirstOrDefault(status => status.name == intensityEffect.name)?.count ?? 0;
    }
}