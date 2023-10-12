using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeOut : MonoBehaviour
{
    public Color startColor;
    public Color endColor;
    public float t;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine(CoFade());
    }

    IEnumerator CoFade()
    {
        float time = 0.0f;

        while (time < t)
        {
            time += Time.deltaTime;
            text.color = Color.Lerp(startColor, endColor, time / t);

            yield return null;
        }
        gameObject.SetActive(false);
    }
}
