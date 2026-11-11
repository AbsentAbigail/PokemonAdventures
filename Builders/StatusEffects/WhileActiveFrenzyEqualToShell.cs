using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Items;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhileActiveFrenzyEqualToShell : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectWhileActiveX>(Name)
            .WithText("While active, has bonus <keyword=frenzy> equal to <keyword=shell>")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectWhileActiveX>(status =>
            {
                status.effectToApply = Mod.GetStatus("MultiHit");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.scriptableAmount = new Script<ScriptableCurrentStatus>(script =>
                {
                    script.statusType = "shell";
                });
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}