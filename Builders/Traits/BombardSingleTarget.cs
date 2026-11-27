using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.Traits;

[UsedImplicitly]
public class BombardSingleTarget : ITraitBuilder
{
    public DataFileBuilder<TraitData, TraitDataBuilder> Builder()
    {
        return new TraitDataBuilder(Mod.Instance)
            .Create(Name)
            .SubscribeToAfterAllBuildEvent(trait =>
            {
                trait.effects = [Mod.GetStatus(StatusEffects.BombardSingleTarget.Name)];
                trait.keyword = Mod.GetKeyword("bombard");
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}