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
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI confirmText;
    public TextMeshProUGUI noticeText;

    public GameObject equipButton;

    private void Start()
    {
        if (PlayDataManager.data == null)
        {
            PlayDataManager.Init();
        }
        equipWeapon = PlayDataManager.data.EquipWeapon;

        UnlockPanel();

        if (BGMobject.instance != null)
        {
            BGMobject.instance.Stop();
        }
    }

    public void SelectWeapon(WeaponID id)
    {
        selectWeapon = id;

        var table = CsvTableMgr.GetTable<ArsenalTable>().dataTable;
        titleText.text = table[id].TITLE;
        infoText.text = table[id].INFO;
    }

    public void UnlockPanel()
    {
        foreach (var weapon in weapons)
        {
            if (PlayDataManager.data.UnlockList.Contains(weapon.ID))
            {
                weapon.Unlock();
                weapon.Equip(equipWeapon);
            }
        }
    }

    public void SelectUnlock()
    {
        isUnlock = true;
        equipText.text = "장착";
        confirmText.text = "장비를 장착 하시겠습니까?";

        if (selectWeapon == equipWeapon)
        {
            equipButton.SetActive(false);
        }
        else
        {
            equipButton.SetActive(true);
        }
    }

    public void SelectLock()
    {
        isUnlock = false;
        equipText.text = "해금";
        confirmText.text = "장비를 해금 하시겠습니까?";
    }

    public void Confirm()
    {
        if (isUnlock)
        {
            equipWeapon = selectWeapon;
            PlayDataManager.data.EquipWeapon = equipWeapon;
            PlayDataManager.Save();

            Notice("장비를 장착하였습니다.");
            UnlockPanel();
            if (selectWeapon == equipWeapon)
            {
                equipButton.SetActive(false);
            }
            else
            {
                equipButton.SetActive(true);
            }
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
            Notice("해금에 성공하였습니다.");
            SelectUnlock();
            UnlockPanel();
        }
        else
        {
            Notice("해금에 실패하였습니다.");
        }
    }

    public void GoLobby()
    {
        if (BGMobject.instance != null)
        {
            BGMobject.instance.Play();
        }
    }
}
