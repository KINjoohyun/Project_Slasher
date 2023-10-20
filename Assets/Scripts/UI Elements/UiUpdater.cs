using System.Collections.Generic;
using UnityEngine;

public class UiUpdater : MonoBehaviour
{
    public List<GameObject> list;

    private void Start()
    {
        UpdateAll();
    }

    public void UpdateAll()
    {
        foreach (var item in list) 
        {
            item.GetComponent<IUpdate>().UpdateUi();
        }
    }

    public void UpdateSelect(IUpdate item)
    {
        item.UpdateUi();
    }
}
