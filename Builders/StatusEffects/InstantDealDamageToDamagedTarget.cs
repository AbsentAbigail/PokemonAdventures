using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Items;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class InstantDealDamageToDamagedTarget : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectInstantDealDamage>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantDealDamage>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;

                status.dealDamage = true;
                status.countsAsHit = true;

                status.targetConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintDamaged>(),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}