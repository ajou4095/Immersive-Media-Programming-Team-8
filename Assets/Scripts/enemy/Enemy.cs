using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public int maxHp = 0;
    public int hp = 0;

    public GameObject attackIcon, healIcon;

    public Action action;
    public GameObject icon;

    public Action NewAction()
    {
        if (hp * 2 < maxHp && Random.value < 0.5f)
        {
            action = new Heal(5, healIcon);
        }
        else
        {
            action = new Attack(5, attackIcon);
        }

        if (icon == null)
        {
            var position = transform.position + new Vector3(0, 1, 0);
            Instantiate(action.getIcon(), position, transform.rotation);
        }

        return action;
    }
}
