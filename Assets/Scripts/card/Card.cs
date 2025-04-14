using UnityEngine;

public class Card : MonoBehaviour
{
    public CardType type;
    public int power;
    public BattleSystem battleSystem;

    private void OnMouseDown()
    {
        if (battleSystem != null)
        {
            battleSystem.UseCard(this);
        }
    }
}
