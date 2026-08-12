using System.Collections.Generic;
using System.Linq;

namespace PokemonMod;

public static class Evolutions
{
    public static StatusEffectInstantSplit.Profile[] Profiles;
    public static readonly List<(string, string)> EvolutionPairs = [
        ("Frostinger", "Grink"),
        ("BabySnowbo", "Snowbo"),
    ];

    public static void Setup()
    {
        Profiles = EvolutionPairs.Select(tuple => Profile(tuple.Item1, tuple.Item2)).ToArray();
    }
    
    private static StatusEffectInstantSplit.Profile Profile(string card, string changeTo)
    {
        return new StatusEffectInstantSplit.Profile { cardName = Mod.GetCard(card).name, changeToCardName = Mod.GetCard(changeTo).name };
    }
}