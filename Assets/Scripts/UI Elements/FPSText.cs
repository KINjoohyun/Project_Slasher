using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSText : MonoBehaviour
{
    private TextMeshProUGUI fpsText;

    private void Awake()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        fpsText.text = $"FPS : {1 / Time.deltaTime}";
    }
}
