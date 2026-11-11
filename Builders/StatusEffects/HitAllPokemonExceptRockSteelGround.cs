using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Scriptables.TargetModes;
using Types = PokemonMod.Variables.Types;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class HitAllPokemonExceptRockSteelGround : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectChangeTargetMode>(Name)
            .WithText($"Hits all Pokemon, excluding <sprite name={Types.Rock.Keyword()}>, <sprite name={Types.Ground.Keyword()}>, and <sprite name={Types.Steel.Keyword()}>")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectChangeTargetMode>(status =>
            {
                status.targetMode = new Script<TargetModeAlliesAndEnemies>(t =>
                {
                    t.constraints = [
                        TargetConstraintHelper.IsPokemon(),
                        TargetConstraintHelper.IsOfTypes([
                            Types.Rock.Name,
                            Types.Steel.Name,
                            Types.Ground.Name,
                    ], true)];
                });
            });
    }
}