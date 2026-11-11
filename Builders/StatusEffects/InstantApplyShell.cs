using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class InstantApplyShell : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectInstantApplyEffect>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantApplyEffect>(status =>
            {
                var s = Mod.GetStatus("Shell");
                status.textKey = s.textKey;
                status.effectToApply = s;
                status.applyFormatKey = s.applyFormatKey;
                status.keyword = "shell";
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}