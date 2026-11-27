using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class TriggerAgainstWhenDemonizeApplied : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectTriggerWhenStatusApplied>(Name)
            .WithText("Trigger against anything that is hit with <keyword=demonize>")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .IsReaction()
            .SubscribeToAfterAllBuildEvent<StatusEffectTriggerWhenStatusApplied>(status =>
            {
                status.targetStatus = Mod.GetStatus("Demonize");
                status.triggerType = StatusEffectTriggerWhenStatusApplied.TriggerType.Target;
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}