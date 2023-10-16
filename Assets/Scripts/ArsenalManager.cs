using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum WeaponID
{
    None = -1,

    Starter = 101,
    Red,
    Yellow,
    Green,
    Healty,
    Random,
    Blood,
    Rapier,
    Iced,
    Death,

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

    public WeaponPanel[] weapons;
    public WeaponID selectWeapon = WeaponID.None;
    public WeaponID equipWeapon = WeaponID.None;
    private bool isUnlock = false;
    public TextMeshProUGUI equipText;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI confirmText;
    public TextMeshProUGUI noticeText;

    private void Start()
    {
        PlayDataManager.Init();
        equipWeapon = PlayDataManager.data.EquipWeapon;

        UnlockPanel();
    }

    public void SelectWeapon(WeaponID id)
    {
        selectWeapon = id;

        var table = CsvTableMgr.GetTable<ArsenalTable>().dataTable;
        infoText.text = table[id].INFO;
    }

    public void UnlockPanel()
    {
        foreach (var weapon in weapons)
        {
            if (PlayDataManager.data.UnlockList.Contains(weapon.ID))
            {
                weapon.Unlock();
            }
        }
    }

    public void SelectUnlock()
    {
        isUnlock = true;
        equipText.text = "����";
        confirmText.text = "��� ���� �Ͻðڽ��ϱ�?";
    }

    public void SelectLock()
    {
        isUnlock = false;
        equipText.text = "�ر�";
        confirmText.text = "��� �ر� �Ͻðڽ��ϱ�?";
    }

    public void Confirm()
    {
        if (isUnlock)
        {
            PlayDataManager.data.EquipWeapon = equipWeapon;
            PlayDataManager.Save();

            Notice("��� �����Ͽ����ϴ�.");
        }
        else
        {
            UnlockWeapon();
        }
    }

    public void Notice(string str)
    {
        noticeText.text = str;
        noticeText.gameObject.SetActive(true);
    }

    public void UnlockWeapon()
    {
        if (PlayDataManager.UnlockWeapon(selectWeapon))
        {
            Notice("�رݿ� �����Ͽ����ϴ�.");
        }
        else
        {
            Notice("�رݿ� �����Ͽ����ϴ�.");
        }
    }
}
