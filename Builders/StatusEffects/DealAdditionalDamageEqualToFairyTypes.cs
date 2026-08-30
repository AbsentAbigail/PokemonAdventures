using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Scriptables.ScriptableAmounts;
using PokemonMod.StatusEffectImplementations;
using PokemonMod.Variables;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class DealAdditionalDamageEqualToFairyTypes : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectDealAdditionalDamage>(Name)
            .WithText($"Deal additional damage equal to <sprite name={Types.Fairy.Keyword()}> in play")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectDealAdditionalDamage>(status =>
            {
                status.scriptableAmount = new Script<ScriptableTargetsOnBoard>(script =>
                {
                    script.all = true;
                    script.statusEffect = Mod.GetStatus(Types.Fairy.Name);
                });
            });
    }
}