using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CsvTableMgr
{
    private static Dictionary<Type, CsvTable> tables = new Dictionary<Type, CsvTable>();

    static CsvTableMgr()
    {
        tables.Clear();

        var upgradeTable = new UpgradeTable();
        tables.Add(typeof(UpgradeTable), upgradeTable);
    }

    public static T GetTable<T>() where T : CsvTable
    {
        var id = typeof(T);
        if (!tables.ContainsKey(id))
        {
            return null;
        }

        return tables[id] as T;
    }
}
