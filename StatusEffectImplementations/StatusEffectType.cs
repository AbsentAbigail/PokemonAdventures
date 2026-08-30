namespace PokemonMod.StatusEffectImplementations;

public class StatusEffectType : StatusEffectData
{
    public int weaknessModifier = 2;
    public int resistanceModifier = -2;
    public int immunityModifier = -4;
    
    public string[] weakTypes;
    public string[] resistingTypes;
    public string[] immuneTypes;
}