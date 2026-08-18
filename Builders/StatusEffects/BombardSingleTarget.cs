using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.FieldEffects;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using UnityEngine;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class BombardSingleTarget : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectBombard>(Name)
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectBombard>(status =>
            {
                status.targetCountRange = new Vector2Int(1, 1);
                status.hitFriendlyChance = 0f;
                status.maxFrontTargets = 1;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}