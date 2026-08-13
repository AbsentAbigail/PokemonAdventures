using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders. StatusEffects;

[UsedImplicitly]
public class ImmuneToBomDemonizeSpiceFrost : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectImmuneToStatus>(Name)
            .WithText("Immune to <keyword=weakness>, <keyword=demonize>, <keyword=spice>, and <keyword=frost>")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectImmuneToStatus>(status =>
            {
                status.replaceWith = Mod.GetStatus(InstantDoNothing.Name);
                status.debuffs = false;
                status.includeTypes = ["vim", "demonize", "spice", "frost"];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}