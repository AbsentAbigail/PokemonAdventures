using System.Linq;

namespace PokemonMod.Scriptables.ScriptableAmounts;

internal class ScriptableTargetsOnBoard : ScriptableAmount
{
    public bool allies;
    public bool enemies;
    public bool inRow;
    public CardType cardType = null;

    public override int Get(Entity entity)
    {
        var result = 0;
        var rows = References.Battle.GetRowIndices(entity);

        if (inRow)
        {
            return InRow(entity, rows);
        }

        if (allies)
        {
            result += entity.GetAllies().Count(e => cardType is null || e.data.cardType == cardType);
        }
        if (enemies)
        {
            result += entity.GetEnemies().Count(e => cardType is null || e.data.cardType == cardType);
        }
        return result;
    }

    private int InRow(Entity entity, int[] rows)
    {
        var result = 0;
        foreach (var row in rows)
        {
            if (allies)
            {
                result += entity.GetAlliesInRow(row).Count(e => cardType is null || e.data.cardType == cardType);
            }
            if (enemies)
            {
                result += entity.GetEnemiesInRow(row).Count(e => cardType is null || e.data.cardType == cardType);
            }
        }

        return result;
    }
}