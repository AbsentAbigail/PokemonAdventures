using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Scriptables.TargetConstraints;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class InstantCountUpStatuses : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectInstantCountUpStatus>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantCountUpStatus>(status =>
            {
                status.negative = true;
                status.positive = true;
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}