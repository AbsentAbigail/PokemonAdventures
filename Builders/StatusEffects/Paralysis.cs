using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.StatusEffectImplementations;
using WildfrostHopeMod.VFX;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class Paralysis : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectParalysis>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectParalysis>(status =>
            {
                status.type = "paralysis";
                status.offensive = true;
                status.removeOnDiscard = true;
            })
            .Subscribe_WithStatusIcon("paralysis");
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}