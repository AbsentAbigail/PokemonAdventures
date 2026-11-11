using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.StatusEffectImplementations;
using WildfrostHopeMod.VFX;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class Lum : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectLumBerry>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectLumBerry>(status =>
            {
                status.doNothing = Mod.GetStatus(InstantDoNothing.Name);
                status.type = "lum";
                status.eventPriority = -1000; // act after other when debuffed effects
            })
            .Subscribe_WithStatusIcon("lum");
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}