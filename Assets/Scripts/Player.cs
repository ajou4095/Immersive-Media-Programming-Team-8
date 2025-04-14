using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public int maxHp = 100;
    public int hp = 100;

    public Dictionary<CardType, int> cardCounts = new();
    private Dictionary<CardType, int> _cardBackup = new();

    private void Start()
    {
        cardCounts[CardType.Attack] = 2;
        cardCounts[CardType.Defense] = 2;
        cardCounts[CardType.Explosion] = 2;

        BackupCards();
    }

    public void BackupCards()
    {
        _cardBackup = new Dictionary<CardType, int>(cardCounts);
    }

    public void ResetCards()
    {
        cardCounts = new Dictionary<CardType, int>(_cardBackup);
    }

    public void AddCardReward(CardType type)
    {
        cardCounts[type]++;
    }

    public bool UseCard(CardType type)
    {
        if (cardCounts.ContainsKey(type) && cardCounts[type] > 0)
        {
            cardCounts[type]--;
            return true;
        }
        return false;
    }
}
