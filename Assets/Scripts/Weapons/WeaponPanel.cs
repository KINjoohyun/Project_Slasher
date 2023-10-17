using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponPanel : MonoBehaviour
{
    public WeaponID ID;
    public GameObject locker;
    public TextMeshProUGUI priceText;

    private void Awake()
    {
        var table = CsvTableMgr.GetTable<ArsenalTable>().dataTable;
        priceText.text = table[ID].PRICE.ToString();
    }

    public void Unlock()
    {
        locker.SetActive(false);
        priceText.text = "����";
    }

    public void Equip(WeaponID id)
    {
        if (ID == id)
        {
            priceText.text = "������";
        }
    }

    public void Select()
    {
        ArsenalManager.instance.SelectWeapon(ID);
    }
}
