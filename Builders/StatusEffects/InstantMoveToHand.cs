using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class InstantMoveToHand : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectInstantMoveToHand>(Name)
            .WithText("Move target to hand")
            .WithStackable(false)
            .WithCanBeBoosted(false);
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}