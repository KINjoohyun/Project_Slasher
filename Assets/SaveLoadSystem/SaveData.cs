using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class SaveData
{
    public int Version { get; set; }

    public abstract SaveData VersionUp();
}

public class SaveDataV1 : SaveData
{
    public SaveDataV1() 
    {
        Version = 1;
    }

    public int HighScore { get; set; }

    public override SaveData VersionUp()
    {
        var data = new SaveDataV2();
        data.HighScore = HighScore;
        
        return data;
    }
}

public class SaveDataV2 : SaveData
{
    public SaveDataV2()
    {
        Version = 2;
    }

    public int HighScore { get; set; }
    public int Gold { get; set; } = 0;

    public override SaveData VersionUp()
    {
        var data = new SaveDataV3();
        data.HighScore = HighScore;
        data.Gold = Gold;

        return data;
    }
}

public class SaveDataV3 : SaveData
{
    public SaveDataV3()
    {
        Version = 3;
    }

    public int HighScore { get; set; }
    public int Gold { get; set; } = 0;

    public override SaveData VersionUp()
    {
        return null;
    }
}