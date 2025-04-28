using UnityEngine;

public class MonsterTouch : MonoBehaviour
{
    private GameObject playerPrefab;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
    }

    public void Initialize(GameObject playerPrefab)
    {
        this.playerPrefab = playerPrefab;
    }

    private void OnMouseDown()
    {
        if (playerPrefab == null)
            return;
        
        _gameManager.NewBattle(this.gameObject);
    }
}
