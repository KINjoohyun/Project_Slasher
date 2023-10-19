using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum UpgradeType
{
    None = -1,

    HealthUP,
    GoldUP,
    SpeedDown,

    Count
}

public class UpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI valueText;
    public TextMeshProUGUI noticeText;
    public Image purchaseIcon;
    public UpgradeType upgradeType = UpgradeType.None;
    public UpgradePanelUiController[] panels;

    private void Start()
    {
        PlayDataManager.Init();

        UpdateUi();

        if (BGMobject.instance != null)
        {
            BGMobject.instance.Stop();
        }
    }

    public void UpdateUi()
    {
        UpdateGold();
        UpdatePanels();
        UpdateValue();
    }

    public void UpdateGold()
    {
        goldText.text = PlayDataManager.data.Gold.ToString();
    }

    public void UpdatePanels()
    {
        foreach (var panel in panels)
        {
            panel.UpdateUi();
        }
    }

    public void UpdateValue()
    {
        switch (upgradeType)
        {
            case UpgradeType.HealthUP:
                valueText.text = $"{PlayDataManager.data.Upgrade_HealthUP} → {PlayDataManager.data.Upgrade_HealthUP + 1}";
                break;

            case UpgradeType.GoldUP:
                valueText.text = $"{PlayDataManager.data.Upgrade_GoldUP} → {PlayDataManager.data.Upgrade_GoldUP + 1}";
                break;

            case UpgradeType.SpeedDown:
                valueText.text = $"{PlayDataManager.data.Upgrade_SpeedDown} → {PlayDataManager.data.Upgrade_SpeedDown + 1}";
                break;

            default:
                valueText.text = string.Empty;
                break;
        }
    }

    public void Notice(string str)
    {
        noticeText.gameObject.SetActive(true);
        noticeText.text = str;
    }

    public void IconHandling(Sprite sprite)
    {
        purchaseIcon.sprite = sprite;
    }

    public void SetUpgrade(int t)
    {
        upgradeType = (UpgradeType)t;

        UpdateValue();
    }

    public void PurchaseUpgrade()
    {
        int pay = 0;
        int level = 0;
        var table = CsvTableMgr.GetTable<UpgradeTable>();
        switch (upgradeType)
        {
            case UpgradeType.HealthUP:
                level = PlayDataManager.data.Upgrade_HealthUP;
                if (!table.IsHealthUPExist(level + 1))
                {
                    Notice("최대 레벨에 도달하였습니다.");
                    return;
                }
                pay = table.healthTable[level + 1].COST;

                break;
            case UpgradeType.GoldUP:
                level = PlayDataManager.data.Upgrade_GoldUP;
                if (!table.IsGoldUPExist(level + 1))
                {
                    Notice("최대 레벨에 도달하였습니다.");
                    return;
                }
                pay = table.goldTable[level + 1].COST;

                break;
            case UpgradeType.SpeedDown:
                level = PlayDataManager.data.Upgrade_SpeedDown;
                if (!table.IsSpeedDownExist(level + 1))
                {
                    Notice("최대 레벨에 도달하였습니다.");
                    return;
                }
                pay = table.speedTable[level + 1].COST;

                break;
            default:
                Notice("잘못된 입력입니다.");
                return;
        }


        if (!PlayDataManager.Purchase(pay))
        {
            Notice("잔액이 부족합니다.");
            return;
        }

        switch (upgradeType)
        {
            case UpgradeType.HealthUP:
                PlayDataManager.data.Upgrade_HealthUP++;

                break;
            case UpgradeType.GoldUP:
                PlayDataManager.data.Upgrade_GoldUP++;

                break;
            case UpgradeType.SpeedDown:
                PlayDataManager.data.Upgrade_SpeedDown++;

                break;
        }
        PlayDataManager.Save();

        UpdateUi();
        Notice("구매하셨습니다.");

    }

    public void GoLobby()
    {
        if (BGMobject.instance != null)
        {
            BGMobject.instance.Play();
        }
    }
}
