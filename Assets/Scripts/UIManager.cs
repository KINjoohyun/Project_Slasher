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

    public void UpdateUI()
    {
        UpdateHP();
        UpdateScore();
    }

    public void UpdateHP()
    {
        hp.value = (float)GameManager.instance.hp / GameManager.instance.maxHp;
    }

    public void UpdateScore() 
    {
        scoreText.text = $"Score : {GameManager.instance.Score}";
    }
}
