using UnityEngine.PlayerLoop;
using SaveDataVC = SaveDataV2; // Version Change?

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
        data.Gold += GameManager.instance.Score;
        Save();
    }
}