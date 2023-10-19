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

        string info = "최대 레벨에 도달하였습니다.";
        switch (upgradeType)
        {
            case UpgradeType.HealthUP:
                UpgradeTable.Data_HealthUP data1 = new UpgradeTable.Data_HealthUP(0, 0, string.Empty);
                if (table.healthTable.TryGetValue(level + 1, out data1))
                {
                    info = data1.INFO;
                }
                break;
            case UpgradeType.GoldUP:
                UpgradeTable.Data_GoldUP data2 = new UpgradeTable.Data_GoldUP(0, 0, string.Empty);
                if (table.goldTable.TryGetValue(level + 1, out data2))
                {
                    info = data2.INFO;
                }
                break;
            case UpgradeType.SpeedDown:
                UpgradeTable.Data_SpeedDown data3 = new UpgradeTable.Data_SpeedDown(0, 0, string.Empty);
                if (table.speedTable.TryGetValue(level + 1, out data3))
                {
                    info = data3.INFO;
                }
                break;
            default:
                return;
                break;
        }
        infoText.text = info;
    }

}
