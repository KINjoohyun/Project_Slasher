using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldUi : MonoBehaviour, IUpdate
{
    private TextMeshProUGUI goldText;

    private void Awake()
    {
        goldText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateUi()
    {
        goldText.text = PlayDataManager.data.Gold.ToString();
    }
}
