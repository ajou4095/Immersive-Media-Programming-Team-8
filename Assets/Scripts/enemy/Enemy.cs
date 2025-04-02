using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour
{
    public int maxHp = 0;
    public int hp = 0;

    public Action action;
    public GameObject icon;

    public Enemy(int maxHp)
    {
        this.maxHp = maxHp;
        hp = this.maxHp;
    }

    public abstract Action NewAction();
}
