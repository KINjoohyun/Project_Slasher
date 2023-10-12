using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CsvTableList", menuName = "ScriptableObject/CsvTableList")]
public class CsvTableListSO : ScriptableObject
{
    public TextAsset[] csvFiles;
}