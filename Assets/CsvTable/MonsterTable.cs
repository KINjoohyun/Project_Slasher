using CsvHelper.Configuration;
using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class MonsterTable : CsvTable
{
    public class Data
    {
        public string ID { get; set; }
        public string NAME { get; set; }
        public string STAGE { get; set; }
        public string MinSPD { get; set; }
        public string MaxSPD { get; set; }
        public string MinPTN { get; set; }
        public string MaxPTN { get; set; }
        public string MinSpwTime { get; set; }
        public string MaxSpwTime { get; set; }
        public string SCORE { get; set; }
        public string DAMAGE { get; set; }
    }

    public class Data_Monster
    {
        public float MinSPD;
        public float MaxSPD;
        public int MinPTN;
        public int MaxPTN;
        public float MinSpwTime;
        public float MaxSpwTime;
        public int SCORE;
        public int DAMAGE;

        public Data_Monster(float minS, float maxS, int minP, int maxP, float minST, float maxST, int score, int damage)
        {
            MinSPD = minS;
            MaxSPD = maxS;
            MinPTN = minP;
            MaxPTN = maxP;
            MinSpwTime = minST;
            MaxSpwTime = maxST;
            SCORE = score;
            DAMAGE = damage;
        }
    }
    public Dictionary<MonsterID, Data_Monster> dataTable = new Dictionary<MonsterID, Data_Monster>();

    public MonsterTable()
    {
        path = "Tables/Monster_table";
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
            dataTable.Add((MonsterID)int.Parse(record.ID), 
                new Data_Monster(float.Parse(record.MinSPD), float.Parse(record.MaxSPD), int.Parse(record.MinPTN), int.Parse(record.MaxPTN),
                float.Parse(record.MinSpwTime), float.Parse(record.MaxSpwTime), int.Parse(record.SCORE), int.Parse(record.DAMAGE)));
        }
    }
}
