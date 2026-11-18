using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;
using PokemonMod.Variables;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhileActiveIceAlliesTakeLessDamage : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectReduceDamageTaken>(Name)
            .WithText($"While active, <sprite name={Types.Ice.Keyword()}> Pokemon take <{{a}}> less damage")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectReduceDamageTaken>(status =>
            {
                status.self = false;
                status.allies = true;
                status.enemies = true;
                status.constraints =
                [
                    TargetConstraintHelper.IsOfTypes([Types.Ice.Name]),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}