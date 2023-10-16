using CsvHelper.Configuration;
using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using static UpgradeTable;

public class ArsenalTable : CsvTable
{
    public class Data
    {
        public string ID { get; set; }
        public string NAME { get; set; }
        public string PRICE { get; set; }
        public string INFO { get; set; }
    }
    public class Data_Weapon
    {
        public string NAME { get; set; }
        public int PRICE { get; set; }
        public string INFO { get; set; }

        public Data_Weapon(string name, int price, string info)
        {
            NAME = name;
            PRICE = price;
            INFO = info;
        }
    }
    public Dictionary<WeaponID, Data_Weapon> dataTable = new Dictionary<WeaponID, Data_Weapon>();

    public ArsenalTable()
    {
        path = "Tables/Arsenal_table";
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
            dataTable.Add((WeaponID)int.Parse(record.ID), new Data_Weapon(record.NAME, int.Parse(record.PRICE), record.INFO));
        }
    }
}
