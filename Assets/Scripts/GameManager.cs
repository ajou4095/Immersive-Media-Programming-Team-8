using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameCam;
    public Player player;
    public Enemy enemy;

    public GameObject battleSystem;

    public GameObject menuPanel;
    public GameObject gamePanel;

    public TextMeshProUGUI AttackcardTxt;
    public TextMeshProUGUI DefencecardTxt;
    public TextMeshProUGUI ExplosioncardTxt;

    public Image Attackcard;
    public Image Defencecard;
    public Image Explosioncard;

    public RectTransform EnemyGroup;
    public RectTransform EnemyHealthBar;

    public RectTransform PlayerGroup;
    public RectTransform PlayerHealthBar;


    public void GameStart()
    {
        gameCam.SetActive(true);

        menuPanel.SetActive(false);

        player.gameObject.SetActive(true);
    }

    void LateUpdate()
    {
        AttackcardTxt.text = player.cardCounts[CardType.Attack].ToString();
        DefencecardTxt.text = player.cardCounts[CardType.Defense].ToString();
        ExplosioncardTxt.text = player.cardCounts[CardType.Explosion].ToString();

        Attackcard.color = new Color(1, 1, 1, (player.cardCounts[CardType.Attack] != 0) ? 1 : 0);
        Defencecard.color = new Color(1, 1, 1, (player.cardCounts[CardType.Defense] != 0) ? 1 : 0);
        Explosioncard.color = new Color(1, 1, 1, (player.cardCounts[CardType.Explosion] != 0) ? 1 : 0);

        if (enemy.maxHp > 0)
            EnemyHealthBar.localScale = new Vector3(enemy.hp / enemy.maxHp, 1, 1);
        else
            EnemyHealthBar.localScale = new Vector3(0, 1, 1);

        if (player.maxHp > 0)
            PlayerHealthBar.localScale = new Vector3(player.hp / player.maxHp, 1, 1);
        else
            PlayerHealthBar.localScale = new Vector3(0, 1, 1);
        

    }

    public void NewBattle(GameObject enemy)
    {
        battleSystem.SetActive(true);
        battleSystem.GetComponent<BattleSystem>().enemy = enemy.GetComponent<Enemy>();
        gamePanel.SetActive(true);
    }
}
