using System;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public Player player = null;
    public Enemy enemy = null;

    private Turn turn = Turn.PlayerWait;
    
    private Texture2D _redTexture, _grayTexture;
    private SoundPlayer _soundPlayer;
    
    private void Start()
    {
        _soundPlayer = GameObject.FindWithTag("SoundPlayer").GetComponent<SoundPlayer>();
        
        // enemy initialize
        // enemy.NewAction();
        // player initialize
    }

    // Update is called once per frame
    private void Update()
    {
        if (turn == Turn.PlayerAnimation)
        {
            // Card Effect
        }
        if (turn == Turn.EnemyAnimation)
        {
            // Enemy Action Effect
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
                if (enemy.action is Attack attackAction)
                {
                    player.hp -= attackAction.amount;
                    _soundPlayer.punch.Play();
                }
                if (enemy.action is Heal healAction)
                {
                    enemy.hp = Math.Max(enemy.hp + healAction.amount, enemy.maxHp);
                    // playSound
                }
                turn = Turn.EnemyAnimation;
                break;
            
            case Turn.EnemyAnimation:
                enemy.NewAction();
                turn = Turn.PlayerWait;
                break;
        }
    }
    
    private void OnGUI()
    {
        float margin = 10;
        float xMin = margin;
        float yMin = margin;
        float width = (Screen.width - margin * 2) / 2;
        float height = 20;

        if (!_redTexture)
        {
            _redTexture = new Texture2D(1, 1);
            _redTexture.SetPixel(0,0,UnityEngine.Color.red);
            _redTexture.Apply();
        }

        if (!_grayTexture)
        {
            _grayTexture = new Texture2D(1, 1);
            _grayTexture.SetPixel(0,0,UnityEngine.Color.gray);
            _grayTexture.Apply();
        }
        
        GUI.skin.box.normal.background = _grayTexture;
        GUI.Box(new Rect(xMin, yMin, width, height), GUIContent.none);
        
        GUI.skin.box.normal.background = _redTexture;
        GUI.Box(new Rect(xMin, yMin, width * player.hp / player.maxHp, height), GUIContent.none);
    }
}

internal enum Turn
{
    PlayerWait,
    PlayerAnimation,
    EnemyWait,
    EnemyAnimation
}