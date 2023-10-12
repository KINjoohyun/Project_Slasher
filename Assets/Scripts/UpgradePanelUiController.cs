using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradePanelUiController : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI levelText;
    public UpgradeType upgradeType;
    private int level = 0;

    public void UpdateUi()
    {
        UpdateLevel();
        UpdateInfo();
    }

    public void UpdateLevel()
    {
        switch (upgradeType)
        {
            case UpgradeType.HealthUP:
                level = PlayDataManager.data.Upgrade_HealthUP;
                break;
            case UpgradeType.GoldUP:
                level = PlayDataManager.data.Upgrade_GoldUP;
                break;
            case UpgradeType.SpeedDown:
                level = PlayDataManager.data.Upgrade_SpeedDown;
                break;
            default:
                return;
                break;
        }
        levelText.text = $"현재 레벨 {level}";
    }

    public void UpdateInfo()
    {
        var table = CsvTableMgr.GetTable<UpgradeTable>();
        string info = string.Empty;
        switch (upgradeType)
        {
            case UpgradeType.HealthUP:
                info = table.healthTable[level + 1].INFO;
                break;
            case UpgradeType.GoldUP:
                info = table.goldTable[level + 1].INFO;
                break;
            case UpgradeType.SpeedDown:
                info = table.speedTable[level + 1].INFO;
                break;
            default:
                return;
                break;
        }
        infoText.text = info;
    }

}
