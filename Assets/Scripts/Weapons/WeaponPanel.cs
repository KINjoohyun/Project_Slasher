using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPanel : MonoBehaviour
{
    public WeaponID ID;
    public GameObject locker;
    public Image border;
    public TextMeshProUGUI priceText;

    private void Awake()
    {
        var table = CsvTableMgr.GetTable<ArsenalTable>().dataTable;
        priceText.text = table[ID].PRICE.ToString();
    }

    public void Unlock()
    {
        locker.SetActive(false);
        priceText.text = "보유";
        border.color = Color.white;
    }

    public void Equip(WeaponID id)
    {
        if (ID == id)
        {
            priceText.text = "장착중";
            border.color = Color.red;
        }
    }

    public void Select()
    {
        ArsenalManager.instance.SelectWeapon(ID);
    }
}
