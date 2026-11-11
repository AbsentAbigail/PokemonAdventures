using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Interfaces;
using PokemonMod.Builders.StatusEffects;

namespace PokemonMod.Builders.Traits;

[UsedImplicitly]
public class Download : ITraitBuilder
{
    public DataFileBuilder<TraitData, TraitDataBuilder> Builder()
    {
        return new TraitDataBuilder(Mod.Instance)
            .Create(Name)
            .SubscribeToAfterAllBuildEvent(trait =>
            {
                trait.effects = [
                    Mod.GetStatus(WhenDeployedDownloadFrontEnemy.Name),
                    Mod.GetStatus(WhenDeployedIncreaseOwnStatuses.Name),
                ];
                trait.keyword = Mod.GetKeyword(Keywords.Download.Name);
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}