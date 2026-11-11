using System.Collections.Generic;

namespace PokemonMod.Variables;

public static class PetManager
{
    public static readonly List<string> Pets = [];

    public static void AddPets()
    {
        foreach (var name in Pokedex.Names)
        {
            var id = Mod.PrefixGuid(name);
            if (!Pets.Contains(id))
            {
                continue;
            }
            
            MetaprogressionSystem.Add<string, string>("pets", id, null);
            Pets.Remove(id);
        }

        foreach (var pet in Pets)
        {
            MetaprogressionSystem.Add<string, string>("pets", pet, null);
        }
    }
}