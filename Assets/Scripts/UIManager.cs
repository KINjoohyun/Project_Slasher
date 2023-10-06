using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public Slider hp;

    private void Start()
    {
        
    }

    public void UpdateHP(float value)
    {
        hp.value = value;
    }
}
