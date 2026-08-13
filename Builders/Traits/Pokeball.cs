using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;

namespace PokemonMod.Builders.Traits;

[UsedImplicitly]
public class Pokeball : ITraitBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<TraitData, TraitDataBuilder> Builder()
    {
        return new TraitDataBuilder(Mod.Instance)
            .Create(Name)
            .SubscribeToAfterAllBuildEvent(trait =>
            {
                trait.keyword = Mod.GetKeyword(Keywords.Pokeball.Name);
                trait.effects =
                [
                    Mod.GetStatus(StatusEffects.Pokeball.Name),
                ];
            });
    }
}