using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class OnCardPlayedCleanseElectricAllies : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectApplyXOnCardPlayed>(Name)
            .WithText($"<keyword=cleanse> all <sprite name={Types.Electric.Keyword()}> allies")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnCardPlayed>(status =>
            {
                status.effectToApply = Mod.GetStatus("Cleanse");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
                status.applyConstraints =
                [
                    TargetConstraintHelper.HasStatus(Types.Electric.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}