using JetBrains.Annotations;

namespace PokemonMod.Builders.Interfaces;

[PublicAPI]
public interface ITrainerBuilder : ICardBuilder
{
    MenuTrainerModifier MenuTrainerModifiers { get; }

    public struct MenuTrainerModifier()
    {
        public string MenuSpriteName;
        public string MenuTitle;
        public string[] Partners = [];
    }
}