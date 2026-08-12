using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Scriptables.TargetConstraints;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class InstantChooseEvolution : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectInstantChooseEvolution>(Name)
            .WithText("Evolve target Pokemon")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantChooseEvolution>(status =>
            {
                status.title = Mod.GetLocalizedString(Name);
                status.evolveEffect = Mod.GetStatusOf<StatusEffectInstantEvolve>(InstantEvolve.Name);
                status.targetConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintCanEvolve>(),
                ];
            });
    }

}