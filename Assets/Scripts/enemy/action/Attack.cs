using UnityEngine;

public class Attack : Action
{
    public int amount = 0;

    public GameObject icon;

    public Attack(int amount, GameObject icon)
    {
        this.amount = amount;
        this.icon = icon;
    }

    public GameObject getIcon()
    {
        return icon;
    }
}