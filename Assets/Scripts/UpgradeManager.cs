using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    private void Start()
    {
        PlayDataManager.Init(); // test code

        UpdateUi();
    }

    public void UpdateUi()
    {
        UpdateGold();
    }

    public void UpdateGold()
    {
        goldText.text = PlayDataManager.data.Gold.ToString();
    }

    public void ReturnScene()
    {
        SceneManager.LoadScene("Title"); // Lobby ������ ���� �ʿ�
    }
}
