using System.Collections.Generic;

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

    public int HighScore { get; set; } = 0;

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

    public int HighScore { get; set; } = 0;
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

    public int HighScore { get; set; } = 0;
    public int Gold { get; set; } = 0;
    public int Upgrade_HealthUP { get; set; } = 0;
    public int Upgrade_GoldUP { get; set; } = 0;
    public int Upgrade_SpeedDown { get; set; } = 0;

    public override SaveData VersionUp()
    {
        var data = new SaveDataV4();
        data.HighScore = HighScore;
        data.Gold = Gold;
        data.Upgrade_HealthUP = Upgrade_HealthUP;
        data.Upgrade_GoldUP = Upgrade_GoldUP;
        data.Upgrade_SpeedDown = Upgrade_SpeedDown;

        return data;
    }
}

public class SaveDataV4 : SaveData
{
    public SaveDataV4()
    {
        Version = 4;
    }

    public int HighScore { get; set; } = 0;
    public int Gold { get; set; } = 0;
    public int Upgrade_HealthUP { get; set; } = 0;
    public int Upgrade_GoldUP { get; set; } = 0;
    public int Upgrade_SpeedDown { get; set; } = 0;
    public int Stage { get; set; } = 1;

    public override SaveData VersionUp()
    {
        var data = new SaveDataV5();
        data.HighScore = HighScore;
        data.Gold = Gold;
        data.Upgrade_HealthUP = Upgrade_HealthUP;
        data.Upgrade_GoldUP = Upgrade_GoldUP;
        data.Upgrade_SpeedDown = Upgrade_SpeedDown;
        data.Stage = Stage;

        data.UnlockList.Add(WeaponID.Starter); // 기본 무기

        return data;
    }
}

public class SaveDataV5 : SaveData // Current
{
    public SaveDataV5()
    {
        Version = 5;
    }

    public int HighScore { get; set; } = 0;
    public int Gold { get; set; } = 0;
    public int Upgrade_HealthUP { get; set; } = 0;
    public int Upgrade_GoldUP { get; set; } = 0;
    public int Upgrade_SpeedDown { get; set; } = 0;
    public int Stage { get; set; } = 1;
    public WeaponID EquipWeapon { get; set; } = WeaponID.Starter;
    public List<WeaponID> UnlockList { get; set; } = new List<WeaponID>();

    public override SaveData VersionUp()
    {
        return null;
    }
}