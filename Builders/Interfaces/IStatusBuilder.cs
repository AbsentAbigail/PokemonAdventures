using Deadpan.Enums.Engine.Components.Modding;
using JetBrains.Annotations;

namespace PokemonMod.Builders.Interfaces;

[PublicAPI]
public interface IStatusBuilder : IBuilder<StatusEffectData, StatusEffectDataBuilder>;