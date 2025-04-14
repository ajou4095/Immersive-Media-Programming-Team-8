using System;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    public Player player;
    public Enemy enemy;

    private Turn turn = Turn.PlayerWait;

    private Texture2D _redTexture, _grayTexture;
    private SoundPlayer _soundPlayer;

    [Header("Card Prefabs")]
    public GameObject attackCardPrefab;
    public GameObject defenseCardPrefab;
    public GameObject explosionCardPrefab;

    [Header("Card Spawn Area")]
    public Transform cardSpawnArea;

    [Header("Result Images")]
    public GameObject winImage;
    public GameObject loseImage;

    private void Start()
    {
        _soundPlayer = GameObject.FindWithTag("SoundPlayer").GetComponent<SoundPlayer>();

        enemy.NewAction();
        player.BackupCards();

        SpawnPlayerCards();

        if (winImage != null) winImage.SetActive(false);
        if (loseImage != null) loseImage.SetActive(false);
    }

    private void Update()
    {
       
    }

    public void UseCard(Card card)
    {
        

        switch (card.type)
        {
            case CardType.Attack:
                enemy.hp -= card.power;
                _soundPlayer.punch.Play();
                break;
            case CardType.Defense:
                player.hp = Mathf.Min(player.hp + card.power, player.maxHp);
                break;
            case CardType.Explosion:
                enemy.hp -= card.power * 2;
                break;
        }

        NextTurn();
    }

    private void NextTurn()
    {
        if (player.hp <= 0 || enemy.hp <= 0)
        {
            EndBattle();
            return;
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
                else if (enemy.action is Heal healAction)
                {
                    enemy.hp = Math.Min(enemy.hp + healAction.amount, enemy.maxHp);
                }
                turn = Turn.EnemyAnimation;
                break;
            case Turn.EnemyAnimation:
                enemy.NewAction();
                turn = Turn.PlayerWait;
                break;
        }
    }

    private void EndBattle()
    {
        if (enemy.hp <= 0)
        {
            if (winImage != null) winImage.SetActive(true);

            player.ResetCards();
            CardType randomType = (CardType)UnityEngine.Random.Range(0, 3);
            player.AddCardReward(randomType);
        }
        else if (player.hp <= 0)
        {
            if (loseImage != null) loseImage.SetActive(true);

            player.ResetCards();
        }
    }

    private void SpawnPlayerCards()
    {
        if (cardSpawnArea == null) return;

        foreach (Transform child in cardSpawnArea)
        {
            Destroy(child.gameObject);
        }

        SpawnCard(attackCardPrefab, CardType.Attack);
        SpawnCard(defenseCardPrefab, CardType.Defense);
        SpawnCard(explosionCardPrefab, CardType.Explosion);
    }

    private void SpawnCard(GameObject prefab, CardType type)
    {
        if (prefab == null) return;

        GameObject cardObj = Instantiate(prefab, cardSpawnArea);
        Card card = cardObj.GetComponent<Card>();
        if (card != null)
        {
            card.type = type;
            card.battleSystem = this;
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
            _redTexture.SetPixel(0, 0, Color.red);
            _redTexture.Apply();
        }

        if (!_grayTexture)
        {
            _grayTexture = new Texture2D(1, 1);
            _grayTexture.SetPixel(0, 0, Color.gray);
            _grayTexture.Apply();
        }

        GUI.skin.box.normal.background = _grayTexture;
        GUI.Box(new Rect(xMin, yMin, width, height), GUIContent.none);

        GUI.skin.box.normal.background = _redTexture;
        GUI.Box(new Rect(xMin, yMin, width * player.hp / player.maxHp, height), GUIContent.none);
    }
}

