using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtyWeapon : MonoBehaviour, IWeapon
{
    public void Ability()
    {
        GameManager.instance.maxHp += 2;
        GameManager.instance.hp += 2;
    }

}
