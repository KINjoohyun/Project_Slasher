using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Blinker : MonoBehaviour
{
    private TextMeshProUGUI blinkText;
    private Coroutine coRun;
    private bool IsBlink { get; set; } = true;
    public float duration = 1.0f;

    private void Awake()
    {
        blinkText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (coRun == null)
        {
            coRun = StartCoroutine((IsBlink) ? FadeOut() : FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        float time = 0.0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            blinkText.alpha += Time.deltaTime;

            yield return null;
        }
        IsBlink = true;
        coRun = null;
    }

    private IEnumerator FadeOut()
    {
        float time = 0.0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            blinkText.alpha -= Time.deltaTime;

            yield return null;
        }
        IsBlink = false;
        coRun = null;
    }
}
