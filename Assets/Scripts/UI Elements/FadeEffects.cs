using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeEffects : MonoBehaviour
{
    public float t;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        StartCoroutine(CoFadeIn());
    }

    public void FadeOut(string sceneName)
    {
        StartCoroutine(CoFadeOut(sceneName));
    }

    private IEnumerator CoFadeIn()
    {
        float time = 0.0f;

        while (time < t)
        {
            time += Time.deltaTime;
            image.color = Color.Lerp(Color.black, Color.clear, time / t);

            yield return null;
        }
    }

    private IEnumerator CoFadeOut(string sceneName)
    {
        float time = 0.0f;

        while (time < t)
        {
            time += Time.deltaTime;
            image.color = Color.Lerp(Color.clear, Color.black, time / t);

            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }
}
