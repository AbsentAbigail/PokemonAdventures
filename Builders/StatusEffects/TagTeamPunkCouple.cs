using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class TagTeamPunkCouple : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectStanceChange>(Name)
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectStanceChange>(status =>
            {
                status.firstStance =
                [
                    Mod.SStack(OnCardPlayedApplyDemonizeToEnemiesWithoutDebuffs.Name),
                ];
                status.secondStance =
                [
                    Mod.SStack(OnCardPlayedApplyOverburnToDebuffedEnemies.Name),
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}