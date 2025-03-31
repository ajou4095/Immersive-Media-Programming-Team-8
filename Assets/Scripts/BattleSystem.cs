using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    private Turn turn = Turn.PlayerWait;

    private Enemy enemy = null;
    private Player player = null;
    
    private void Start()
    {
        // enemy setting
        // player setting
    }

    // Update is called once per frame
    private void Update()
    {
        if (turn == Turn.PlayerWait)
        {
            // Card Animation
        }
    }

    private void UseCard(Card card)
    {
        // card.use(enemy);
        NextTurn();
    }

    private void NextTurn()
    {
        if (player.hp == 0)
        {
            // Defeat();
        } else if (enemy.hp == 0)
        {
            // Win();
        }

        switch (turn)
        {
            case Turn.PlayerWait:
                turn = Turn.PlayerAnimation;
                break;
            case Turn.PlayerAnimation:
                turn = Turn.EnemyWait;
                break;
            case Turn.EnemyWait:
                turn = Turn.EnemyAnimation;
                break;
            case Turn.EnemyAnimation:
                turn = Turn.PlayerWait;
                break;
        }
    }
}

internal enum Turn
{
    PlayerWait,
    PlayerAnimation,
    EnemyWait,
    EnemyAnimation
}