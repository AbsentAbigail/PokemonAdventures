namespace PokemonMod;

public static class Types
{
    public static readonly Type Normal = new(NormalId)
    {
        WeakTypes = 
        [
        ],
        ResistingTypes = [
            RockId,
            SteelId,
        ],
        ImmuneTypes = [
            GhostId,
        ],
    };
    public static readonly Type Fighting = new(FightingId)
    {
        WeakTypes = 
        [
            NormalId,
            RockId,
            SteelId,
            IceId,
            DarkId,
        ],
        ResistingTypes = [
            FlyingId,
            PoisonId,
            BugId,
            PsychicId,
            FairyId,
        ],
        ImmuneTypes = [
            GhostId,
        ],
    };
    public static readonly Type Flying = new(FlyingId)
    {
        WeakTypes = 
        [
            FightingId,
            BugId,
            GrassId,
        ],
        ResistingTypes = [
            RockId,
            SteelId,
            ElectricId,
        ],
        ImmuneTypes = [
        ],
    };
    public static readonly Type Poison = new(PoisonId)
    {
        WeakTypes = 
        [
            GrassId,
            FairyId,
        ],
        ResistingTypes = [
            PoisonId,
            GroundId,
            RockId,
            GhostId,
        ],
        ImmuneTypes = [
            SteelId,
        ],
    };
    public static readonly Type Ground = new(GroundId)
    {
        WeakTypes = 
        [
            PoisonId,
            RockId,
            SteelId,
            FireId,
            ElectricId,
        ],
        ResistingTypes = [
            BugId,
            GrassId,
        ],
        ImmuneTypes = [
            FlyingId,
        ],
    };
    public static readonly Type Rock = new(RockId)
    {
        WeakTypes = 
        [
            FlyingId,
            BugId,
            FireId,
            IceId,
        ],
        ResistingTypes = [
            FightingId,
            GroundId,
            SteelId,
        ],
        ImmuneTypes = [
        ],
    };
    public static readonly Type Bug = new(BugId)
    {
        WeakTypes = 
        [
            GrassId,
            PsychicId,
            DarkId,
        ],
        ResistingTypes = [
            FightingId,
            FlyingId,
            PoisonId,
            GhostId,
            SteelId,
            FireId,
            FairyId,
        ],
        ImmuneTypes = [
        ],
    };
    public static readonly Type Ghost = new(GhostId)
    {
        WeakTypes = 
        [
            GhostId,
            PsychicId,
        ],
        ResistingTypes = [
            DarkId,
        ],
        ImmuneTypes = [
            NormalId,
        ],
    };
    public static readonly Type Steel = new(SteelId)
    {
        WeakTypes = 
        [
            RockId,
            IceId,
            FairyId,
        ],
        ResistingTypes = [
            SteelId,
            FireId,
            WaterId,
            ElectricId,
        ],
        ImmuneTypes = [
        ],
    };
    public static readonly Type Fire = new(FireId)
    {
        WeakTypes = 
        [
            BugId,
            SteelId,
            GrassId,
            IceId,
        ],
        ResistingTypes = [
            RockId,
            FireId,
            WaterId,
            DragonId,
        ],
        ImmuneTypes = [
        ],
    };
    public static readonly Type Water = new(WaterId)
    {
        WeakTypes = 
        [
            GroundId,
            RockId,
            FireId,
        ],
        ResistingTypes = [
            WaterId,
            GrassId,
            DragonId,
        ],
        ImmuneTypes = [
        ],
    };
    public static readonly Type Grass = new(GrassId)
    {
        WeakTypes = 
        [
            GroundId,
            WaterId,
            RockId,
        ],
        ResistingTypes = [
            PoisonId,
            DragonId,
            SteelId,
            BugId,
            FireId,
        ],
        ImmuneTypes = [],
    };
    public static readonly Type Electric = new(ElectricId)
    {
        WeakTypes = 
        [
            FlyingId,
            WaterId,
        ],
        ResistingTypes = [
            GrassId,
            ElectricId,
            DragonId,
        ],
        ImmuneTypes = [
            GroundId,
        ],
    };
    public static readonly Type Psychic = new(PsychicId)
    {
        WeakTypes = 
        [
            FightingId,
            PoisonId,
        ],
        ResistingTypes = [
            SteelId,
            PsychicId,
        ],
        ImmuneTypes = [
            DarkId,
        ],
    };
    public static readonly Type Ice = new(IceId)
    {
        WeakTypes = 
        [
            FlyingId,
            GroundId,
            GrassId,
            DragonId,
        ],
        ResistingTypes = [
            SteelId,
            FireId,
            WaterId,
            IceId,
        ],
        ImmuneTypes = [],
    };
    public static readonly Type Dragon = new(DragonId)
    {
        WeakTypes = 
        [
            DragonId,
        ],
        ResistingTypes = [
            SteelId,
        ],
        ImmuneTypes = [
            FairyId,
        ],
    };
    public static readonly Type Dark = new(DarkId)
    {
        WeakTypes = 
        [
            GhostId,
            PsychicId,
        ],
        ResistingTypes = [
            FightingId,
            DarkId,
            FairyId,
        ],
        ImmuneTypes = [],
    };
    public static readonly Type Fairy = new(FairyId)
    {
        WeakTypes = 
        [
            FightingId,
            DragonId,
            DarkId,
        ],
        ResistingTypes = [
            PoisonId,
            SteelId,
            FireId,
        ],
        ImmuneTypes = [],
    };

    private const string NormalId = "NormalType";
    private const string FightingId = "FightingType";
    private const string FlyingId = "FlyingType";
    private const string PoisonId = "PoisonType";
    private const string GroundId = "GroundType";
    private const string RockId = "RockType";
    private const string BugId = "BugType";
    private const string GhostId = "GhostType";
    private const string SteelId = "SteelType";
    private const string FireId = "FireType";
    private const string WaterId = "WaterType";
    private const string GrassId = "GrassType";
    private const string ElectricId = "ElectricType";
    private const string PsychicId = "PsychicType";
    private const string IceId = "IceType";
    private const string DragonId = "DragonType";
    private const string DarkId = "DarkType";
    private const string FairyId = "FairyType";

    public static readonly Type[] TypeChart =
    [
        Normal,
        Fighting,
        Flying,
        Poison,
        Ground,
        Rock,
        Bug,
        Ghost,
        Steel,
        Fire,
        Water,
        Grass,
        Electric,
        Psychic,
        Ice,
        Dragon,
        Dark,
        Fairy,
    ];

    public class Type(string name)
    {
        public readonly string Name = name;
        public string[] WeakTypes;
        public string[] ResistingTypes;
        public string[] ImmuneTypes;

        public string ReadableName()
        {
            return Name.Replace("Type", string.Empty);
        }

        public string Keyword()
        {
            return ReadableName().ToLower();
        }
    }
}