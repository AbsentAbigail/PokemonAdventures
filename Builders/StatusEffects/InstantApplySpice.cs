using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class InstantApplySpice : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectInstantApplyEffect>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantApplyEffect>(status =>
            {
                var s = Mod.GetStatus("Spice");
                status.textKey = s.textKey;
                status.effectToApply = s;
                status.applyFormatKey = s.applyFormatKey;
                status.keyword = "spice";
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}