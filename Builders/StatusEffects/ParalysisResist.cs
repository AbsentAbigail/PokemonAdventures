using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using WildfrostHopeMod.VFX;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class ParalysisResist : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectImmuneToX>(Name)
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectImmuneToX>(status =>
            {
                status.immunityType = "pararlysis";
            })
            .Subscribe_WithStatusIcon("paralysisresist");
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}