using CsvHelper.Configuration;
using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class UpgradeTable : CsvTable
{
    public class Data
    {
        public string ID { get; set; }
        public string NAME { get; set; }
        public string LEVEL { get; set; }
        public string COST { get; set; }
        public string VALUE { get; set; }
        public string INFO { get; set; }
    }

    public class Data_HealthUP
    {
        public int COST;
        public int VALUE;
        public string INFO;

        public Data_HealthUP(int cost, int value, string info)
        {
            COST = cost;
            VALUE = value;
            INFO = info;
        }
    }
    public Dictionary<int, Data_HealthUP> healthTable = new Dictionary<int, Data_HealthUP>();

    public class Data_GoldUP
    {
        public int COST;
        public int VALUE;
        public string INFO;

        public Data_GoldUP(int cost, int value, string info)
        {
            COST = cost;
            VALUE = value;
            INFO = info;
        }
    }
    public Dictionary<int, Data_GoldUP> goldTable = new Dictionary<int, Data_GoldUP>();

    public class Data_SpeedDown
    {
        public int COST;
        public float VALUE;
        public string INFO;

        public Data_SpeedDown(int cost, float value, string info)
        {
            COST = cost;
            VALUE = value;
            INFO = info;
        }
    }
    public Dictionary<int, Data_SpeedDown> speedTable = new Dictionary<int, Data_SpeedDown>();

    public UpgradeTable()
    {
        path = "Tables/Upgrade_table";
        Load();
    }

    public override void Load()
    {
        var csvStr = Resources.Load<TextAsset>(path);
        TextReader reader = new StringReader(csvStr.text);
        var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

        var records = csv.GetRecords<Data>();
        foreach (var record in records)
        {
            //dataTable.Add(record.ID, record.NAME);
            switch (record.NAME)
            {
                case "HealthUP":
                    healthTable.Add(int.Parse(record.LEVEL), new Data_HealthUP(int.Parse(record.COST), int.Parse(record.VALUE), record.INFO));
                    break;
                case "GoldUP":
                    goldTable.Add(int.Parse(record.LEVEL), new Data_GoldUP(int.Parse(record.COST), int.Parse(record.VALUE), record.INFO));
                    break;
                case "SpeedDown":
                    speedTable.Add(int.Parse(record.LEVEL), new Data_SpeedDown(int.Parse(record.COST), float.Parse(record.VALUE), record.INFO));
                    break;
                default:
                    break;
            }
        }
    }

#region HealthUP
    public void OutHealthUP(int level, out int cost, out int value, out string info)
    {
        if (!healthTable.ContainsKey(level))
        {
            cost = 0;
            value = 0;
            info = string.Empty;
            return;
        }
        cost = healthTable[level].COST;
        value = healthTable[level].VALUE;
        info = healthTable[level].INFO;
    }

    public bool IsHealthUPExist(int level)
    {
        return healthTable.ContainsKey(level);
    }
#endregion

#region GoldUP
    public void OutGoldUP(int level, out int cost, out int value, out string info)
    {
        if (!goldTable.ContainsKey(level))
        {
            cost = 0;
            value = 0;
            info = string.Empty;
            return;
        }
        cost = goldTable[level].COST;
        value = goldTable[level].VALUE;
        info = goldTable[level].INFO;
    }

    public bool IsGoldUPExist(int level)
    {
        return goldTable.ContainsKey(level);
    }
#endregion

#region SpeedDown
    public void OutSpeedDown(int level, out int cost, out float value, out string info)
    {
        if (!speedTable.ContainsKey(level))
        {
            cost = 0;
            value = 0.0f;
            info = string.Empty;
            return;
        }
        cost = speedTable[level].COST;
        value = speedTable[level].VALUE;
        info = speedTable[level].INFO;
    }

    public bool IsSpeedDownExist(int level)
    {
        return speedTable.ContainsKey(level);
    }
#endregion


}
