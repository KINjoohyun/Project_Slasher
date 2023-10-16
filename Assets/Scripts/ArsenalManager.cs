using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponID
{
    None = -1,



    Count
}

public class ArsenalManager : MonoBehaviour
{
    public static ArsenalManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameObject[] locks;

    private void Start()
    {
        PlayDataManager.Init();

        Unlock();
    }

    public void SelectWeapon(WeaponID id)
    {

    }

    public void Unlock()
    {
        for (int i = 0; i < PlayDataManager.data.Stage; i++)
        {
            locks[i].SetActive(false);
        }
    }
}
