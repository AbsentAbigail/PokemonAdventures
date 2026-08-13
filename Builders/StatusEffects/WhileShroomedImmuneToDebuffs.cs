using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhileShroomedImmuneToDebuffs : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectImmuneToStatus>(Name)
            .WithText($"While <keyword=shroom>'d, immune to debuffs")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectImmuneToStatus>(status =>
            {
                status.replaceWith = Mod.GetStatus(InstantDoNothing.Name);
                status.debuffs = true;
                status.excludeTypes =
                [
                    "shroom",
                ];
                status.conditions =
                [
                    TargetConstraintHelper.HasStatus("Shroom"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}