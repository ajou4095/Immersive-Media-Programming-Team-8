using UnityEngine;

public class Heal : Action
{
    public int amount = 0;

    public GameObject icon;

    public Heal(int amount, GameObject icon)
    {
        this.amount = amount;
        this.icon = icon;
    }

    public GameObject getIcon()
    {
        return icon;
    }
}