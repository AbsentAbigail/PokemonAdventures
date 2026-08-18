using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Scriptables.ScriptableAmounts;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhileActiveFrenzyEqualToAlliesAndEnemies : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectWhileActiveXUpdateWhenMoved>(Name)
            .WithText("Has bonus <keyword=frenzy> equal to allies and enemies on board")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectWhileActiveXUpdateWhenMoved>(status =>
            {
                status.effectToApply = Mod.GetStatus("MultiHit");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.scriptableAmount = new Script<ScriptableTargetsOnBoard>(scriptable =>
                {
                    scriptable.allies = true;
                    scriptable.enemies = true;
                });
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}