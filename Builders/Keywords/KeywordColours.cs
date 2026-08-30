using UnityEngine;

namespace PokemonMod.Builders.Keywords;

public static class KeywordColours
{
    public static readonly Color Orange = Color(255, 202, 87);
    public static readonly Color White = Color(255, 255, 255);
    public static readonly Color DarkLabel = Color(54, 37, 45);
    public static readonly Color Gray = Color(166, 166, 166);
    public static readonly Color DarkPurple = Color(74, 57, 148);
    public static readonly Color LightPurple = Color(100, 67, 158);
    public static readonly Color Blue = Color(91, 206, 250);
    public static readonly Color Pink = Color(245, 169, 184);
    public static readonly Color Red = Color(195, 37, 35);

    public static readonly Color Rest = Color(253, 97, 195);
    public static readonly Color Scavenge = Color(72, 195, 157);
    public static readonly Color Trample = Color(56, 235, 164);
    public static readonly Color CatColor = Color(220, 60, 60);
    public static readonly Color EtherealColor = new(0.35f, 0.35f, 0.45f);
    public static readonly Color EquipColor = Color(180, 44, 84);
    public static readonly Color Paralysis2 = Color(184, 77, 17);
    public static readonly Color Paralysis = Color(254, 221, 96);
    public static readonly Color Constricted = Color(133, 182, 84);
    public static readonly Color BurnDark = Color(208, 7, 27);
    public static readonly Color BurnLight = Color(255, 182, 109);
    public static readonly Color SleepDark = Color(62, 51, 118);
    public static readonly Color SleepLight = Color(161, 193, 235);
    
    private static Color Color(int r, int g, int b)
    {
        Color color = new(
            r / 255F,
            g / 255F,
            b / 255F
        );

        return color;
    }
}