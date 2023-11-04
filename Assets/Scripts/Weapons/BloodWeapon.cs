using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodWeapon : MonoBehaviour, IWeapon
{
    public int count = 5;
    private int i = 0;

    public void Active()
    {
        GameManager.instance.actionOnKill += () =>
        {
            Drain();
        };
    }

    public void Drain()
    {
        i++;
        if (i >= count)
        {
            i = 0;
            GameManager.instance.hp = Mathf.Clamp(GameManager.instance.hp + 1, 0, GameManager.instance.maxHp + 1);
            UIManager.instance.UpdateHP();
        }
    }
}
