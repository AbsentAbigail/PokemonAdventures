using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.StatusEffectImplementations;
using WildfrostHopeMod.VFX;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class Burn : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectBurn>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectBurn>(status =>
            {
                status.type = "burn";
                status.offensive = true;
                status.removeOnDiscard = true;
            })
            .Subscribe_WithStatusIcon("burn");
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}