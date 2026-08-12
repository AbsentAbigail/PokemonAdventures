using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.StatusEffectImplementations;
using WildfrostHopeMod.VFX;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class Sleep : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectSleep>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectSleep>(status =>
            {
                status.type = "sleep";
                status.offensive = true;
                status.removeOnDiscard = true;
            })
            .Subscribe_WithStatusIcon("sleep");
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}