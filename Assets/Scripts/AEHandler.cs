using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEHandler : MonoBehaviour
{    
    public GameObject target;

    public void DeathEvent()
    {
        if (target != null)
        {
            target.GetComponent<IDeathEvent>().OnDeath();
        }
    }
}