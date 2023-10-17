using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtyWeapon : MonoBehaviour, IWeapon
{
    public int value = 2;

    public void Active()
    {
        GameManager.instance.maxHp += value;
        GameManager.instance.hp += value;
    }

}
