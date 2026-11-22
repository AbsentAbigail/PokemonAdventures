using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using PokemonMod.Builders.Cards.Companions;
using PokemonMod.Builders.Cards.Enemies;
using PokemonMod.Builders.Cards.LeaderPokemon;
using PokemonMod.Builders.Interfaces;
using PokemonMod.StatusEffectImplementations;

namespace PokemonMod.Builders.StatusEffects;

[UsedImplicitly]
public class InstantAddEnemyWave : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Mod.Instance)
            .Create<StatusEffectInstantAddEnemyWave>(Name)
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantAddEnemyWave>(status =>
            {
                status.DefaultWaves =
                [
                    new BattleWaveManager.WaveDataBasic
                    {
                        cards =
                        [
                            new BattleWaveManager.Card(Mod.PrefixGuid(Nidoking.Name)),
                            new BattleWaveManager.Card(Mod.PrefixGuid(Nidoqueen.Name)),
                            new BattleWaveManager.Card(Mod.PrefixGuid(Nidoran.Name)),
                        ],
                    },
                    new BattleWaveManager.WaveDataBasic
                    {
                        cards =
                        [
                            new BattleWaveManager.Card(Mod.PrefixGuid(Blastoise.Name)),
                            new BattleWaveManager.Card(Mod.PrefixGuid(Venusaur.Name)),
                            new BattleWaveManager.Card(Mod.PrefixGuid(Charizard.Name)),
                        ],
                    },
                    new BattleWaveManager.WaveDataBasic
                    {
                        cards =
                        [
                            new BattleWaveManager.Card(Mod.PrefixGuid(Feraligatr.Name)),
                            new BattleWaveManager.Card(Mod.PrefixGuid(Meganium.Name)),
                            new BattleWaveManager.Card(Mod.PrefixGuid(Typhlosion.Name)),
                        ],
                    },
                    new BattleWaveManager.WaveDataBasic
                    {
                        cards =
                        [
                            new BattleWaveManager.Card(Mod.PrefixGuid(Beedrill.Name)),
                            new BattleWaveManager.Card(Mod.PrefixGuid(Butterfree.Name)),
                        ],
                    },
                    new BattleWaveManager.WaveDataBasic
                    {
                        cards =
                        [
                            new BattleWaveManager.Card(Mod.PrefixGuid(Vileplume.Name)),
                            new BattleWaveManager.Card(Mod.PrefixGuid(Bellossom.Name)),
                        ],
                    },
                    new BattleWaveManager.WaveDataBasic
                    {
                        cards =
                        [
                            new BattleWaveManager.Card(Mod.PrefixGuid(Delibird.Name)),
                            new BattleWaveManager.Card(Mod.PrefixGuid(Persian.Name)),
                        ],
                    },
                    new BattleWaveManager.WaveDataBasic
                    {
                        cards =
                        [
                            new BattleWaveManager.Card(Mod.PrefixGuid(Tyranitar.Name)),
                            new BattleWaveManager.Card(Mod.PrefixGuid(Golem.Name)),
                            new BattleWaveManager.Card(Mod.PrefixGuid(Rhyperior.Name)),
                        ],
                    },
                    new BattleWaveManager.WaveDataBasic
                    {
                        cards =
                        [
                            new BattleWaveManager.Card(Mod.PrefixGuid(Porygon.Name)),
                            new BattleWaveManager.Card(Mod.PrefixGuid(Porygon2.Name)),
                            new BattleWaveManager.Card(Mod.PrefixGuid(PorygonZ.Name)),
                        ],
                    },
                    new BattleWaveManager.WaveDataBasic
                    {
                        cards =
                        [
                            new BattleWaveManager.Card(Mod.PrefixGuid(Magnezone.Name)),
                            new BattleWaveManager.Card(Mod.PrefixGuid(Ampharos.Name)),
                        ],
                    },
                    new BattleWaveManager.WaveDataBasic
                    {
                        cards =
                        [
                            new BattleWaveManager.Card(Mod.PrefixGuid(Qwilfish.Name)),
                            new BattleWaveManager.Card(Mod.PrefixGuid(Skarmory.Name)),
                        ],
                    },
                ];
            });
    }

    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}