using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreUi : MonoBehaviour, IUpdate
{
    private TextMeshProUGUI highscoreText;

    private void Awake()
    {
        highscoreText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateUi()
    {
        highscoreText.text = PlayDataManager.data.HighScore.ToString();
    }
}
