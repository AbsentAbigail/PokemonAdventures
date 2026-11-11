using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using WildfrostHopeMod.VFX;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class Insomnia : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectImmuneToX>(Name)
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectImmuneToX>(status =>
            {
                status.immunityType = "sleepresist";
            })
            .Subscribe_WithStatusIcon("sleepresist");
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}