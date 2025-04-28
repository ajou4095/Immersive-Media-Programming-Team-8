using UnityEngine;

public class MonsterTouch : MonoBehaviour
{
    private GameObject battleSystemPrefab;
    private GameObject playerPrefab;

    public void Initialize(GameObject battleSystemPrefab, GameObject playerPrefab)
    {
        this.battleSystemPrefab = battleSystemPrefab;
        this.playerPrefab = playerPrefab;
    }

    private void OnMouseDown()
    {
        if (battleSystemPrefab == null || playerPrefab == null)
            return;

        GameObject battleObject = Instantiate(battleSystemPrefab);
        BattleSystem battleSystem = battleObject.GetComponent<BattleSystem>();

        if (battleSystem != null)
        {
            battleSystem.Player = playerPrefab;
            battleSystem.Enemy = this.gameObject;
        }
    }
}
