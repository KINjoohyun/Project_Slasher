using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public List<ParticleSystem> particlePrefabs;
    public Dictionary<WeaponID, ParticleSystem> weaponList = new Dictionary<WeaponID, ParticleSystem>();

    private void Start()
    {
        PlayDataManager.Init();

        int index = 0;
        for (WeaponID i = WeaponID.Starter; i < WeaponID.Count; i++)
        {
            weaponList.Add(i, particlePrefabs[index]);
            index++;
        }

        SwipeManager.instance.slashParticle = weaponList[PlayDataManager.data.EquipWeapon];
    }

}
