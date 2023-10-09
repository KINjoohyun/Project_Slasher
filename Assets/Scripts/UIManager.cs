using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public Slider hp;
    public TextMeshProUGUI scoreText;
    public GameObject gameoverPanel;
    public GameObject pausePanel;

    public void UpdateUI()
    {
        UpdateHP();
        UpdateScore();
        UpdateGameover();
    }

    public void UpdateHP()
    {
        hp.value = (float)GameManager.instance.hp / GameManager.instance.maxHp;
    }

    public void UpdateScore() 
    {
        scoreText.text = $"Score : {GameManager.instance.Score}";
    }

    public void UpdateGameover()
    {
        if (GameManager.instance.IsGameover)
        {
            gameoverPanel.SetActive(true);
        }
        else
        {
            gameoverPanel.SetActive(false);
        }
    }

    public void UpdatePause()
    {
        if (GameManager.instance.IsPause)
        {
            pausePanel.SetActive(false);
        }
        else
        {
            pausePanel.SetActive(true);
        }
        GameManager.instance.Pause();
    }
}