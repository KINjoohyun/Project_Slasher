using UnityEngine.PlayerLoop;
using SaveDataVC = SaveDataV5; // Version Change?

public static class PlayDataManager
{
    public static SaveDataVC data;

    public static void Init()
    {
        data = SaveLoadSystem.Load("savefile.json") as SaveDataVC;
        if (data == null)
        {
            data = new SaveDataVC();
            SaveLoadSystem.Save(data, "savefile.json");
        }
    }

    public static void Save()
    {
        SaveLoadSystem.Save(data, "savefile.json");
    }

    public static void Reset()
    {
        data = new SaveDataVC();
        Save();
    }

    public static void Gameover()
    {
        data.HighScore = GameManager.instance.HighScore;
        if (data.Upgrade_GoldUP == 0)
        {
            data.Gold += GameManager.instance.Score;
        }
        else
        {
            var table = CsvTableMgr.GetTable<UpgradeTable>();
            data.Gold += GameManager.instance.Score * table.goldTable[data.Upgrade_GoldUP].VALUE;
        }
        Save();
    }

    public static bool Purchase(int pay)
    {
        if (pay > data.Gold)
        {
            return false;
        }

        data.Gold -= pay;
        Save();
        return true;
    }

    public static void UnlockStage(int stage)
    {
        if (data.Stage == stage)
        {
            data.Stage++;
            Save();
        }
    }

    public static bool UnlockWeapon(WeaponID id)
    {
        var table = CsvTableMgr.GetTable<ArsenalTable>();
        if (table.dataTable[id].PRICE > data.Gold)
        {
            return false;
        }

        data.Gold -= table.dataTable[id].PRICE;
        data.UnlockList.Add(id);
        Save();
        return true;
    }
}