using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleManager : MonoBehaviour
{
    public static TitleManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        PlayDataManager.Init();
        GoldPrint();
    }

     // test code
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI highscoreText;

    public void GoldPrint()
    {
        goldText.text = $"GOLD : {PlayDataManager.data.Gold}";
        highscoreText.text = $"HIGH : {PlayDataManager.data.HighScore}";
    }
}