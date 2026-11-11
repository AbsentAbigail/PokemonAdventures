using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Helpers;
using PokemonMod.Variables;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class WhileActiveShroomRetainsAtLeast : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectWhileActiveX>(Name)
            .WithText("While active, <keyword=shroom> does not count below <{a}>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectWhileActiveX>(status =>
            {
                status.effectToApply = Mod.GetStatus(RetainAtLeastShroom.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self 
                                      | StatusEffectApplyX.ApplyToFlags.Allies
                                      | StatusEffectApplyX.ApplyToFlags.Enemies;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}