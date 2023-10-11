using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    public static TestManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public Image guideImage;
    public TextMeshProUGUI similarityText;
    public TextMeshProUGUI inputText;

    private Pattern curPattern = Pattern.None;

    public void SetGuide(Sprite sprite)
    {
        guideImage.sprite = sprite;
    }

    public void SetPattern(int p)
    {
        curPattern = (Pattern)p;
    }

    public void SetSimilarity(Pattern p, float similarity)
    {
        if (curPattern == p)
        {
            similarityText.text = $"{similarity}%";
        }
    }

    public void SetText(Pattern p)
    {
        inputText.text = p.ToString();
    }
}
