using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BloodWeapon : MonoBehaviour, IWeapon
{
    public int count = 5;
    private int i = 0;

    public void Active()
    {
        GameManager.instance.actionOnSlash += () =>
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
            GameManager.instance.hp = math.clamp(GameManager.instance.hp + 1, 0, GameManager.instance.maxHp);
            UIManager.instance.UpdateUI();
        }
    }
}
