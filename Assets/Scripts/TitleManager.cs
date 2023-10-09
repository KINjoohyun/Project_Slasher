using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void GoGame()
    {
        SceneManager.LoadScene("Game");
    }
}