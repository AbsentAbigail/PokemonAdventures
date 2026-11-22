using System.Collections;
using System.Linq;
using PokemonMod.Helpers;

namespace PokemonMod.StatusEffectImplementations;

internal class StatusEffectInstantAddEnemyWave : StatusEffectInstant
{
    public BattleWaveManager.WaveData[] DefaultWaves;
    public bool takeNaturalWave = true;
    public int waveCounter = 2;

    public override IEnumerator Process()
    {
        var waveBell = FindObjectOfType<WaveDeploySystemOverflow>(false);
        if (waveBell == null)
        {
            yield return Remove();
            yield break;
        }

        var wave = takeNaturalWave ? GetWave() : null;
        if (wave == null)
        {
            LogHelper.Log("No valid wave found, resorting to backup");
            wave = new BattleWaveManager.Wave(((StatusEffectInstantAddEnemyWave)original).DefaultWaves.RandomItem());
            foreach (var waveUnit in wave.units.Where(waveUnit => waveUnit.cardType.name != "Enemy"))
            {
                waveUnit.cardType = Mod.GetCardType("Enemy");
            }
            wave.counter = waveCounter;
        }

        var opponent = Battle.GetOpponent(References.Player);
        foreach (var card in wave.units.Select(cardData => CreateCardCopy(cardData, opponent.reserveContainer, Battle.instance.playerCardController)))
        {
            LogHelper.Log("Adding card " + card.entity.data.title);
            yield return card.UpdateData();
            Battle.instance.cards.Add(card.entity);
        }

        if (waveBell.counter == 0)
        {
            waveBell.SetCounter(wave.counter);
        }
        waveBell.waves.Add(wave);
        waveBell.Show();
        yield return Remove();
    }

    private BattleWaveManager.Wave GetWave()
    {
        var node = Campaign.FindCharacterNode(References.Battle.player);

        if (!node.data.TryGetValue("battle", out var obj) || obj is not string assetName)
        {
            return null;
        }

        var battleData = AddressableLoader.Get<BattleData>("BattleData", assetName);
        if (battleData == null)
        {
            return null;
        }

        var possibleWaves = (from wavePool in battleData.pools
            from wave in wavePool.waves
            where wave.units.All(unit => !unit.cardType.miniboss)
            select wave).ToList();

        var randomWave = possibleWaves.RandomItem();
        return new BattleWaveManager.Wave
        {
            counter = waveCounter, isBossWave = false, units = randomWave.units.Select(cardData => cardData.Clone(false)).ToList(),
        };
    }

    private static Card CreateCardCopy(CardData cardData, CardContainer container, CardController controller)
    {
        var card = CardManager.Get(cardData, controller, container.owner, true, false);
        card.entity.flipper.FlipUpInstant();
        card.canvasGroup.alpha = 1.0f;
        container.Add(card.entity);
        var transform = card.transform;
        transform.localPosition = card.entity.GetContainerLocalPosition();
        transform.localEulerAngles = card.entity.GetContainerLocalRotation();
        transform.localScale = card.entity.GetContainerScale();
        container.Remove(card.entity);
        card.entity.owner.reserveContainer.Add(card.entity);
        return card;
    }
}