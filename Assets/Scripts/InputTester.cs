using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InputTester : MonoBehaviour
{
    public TextMeshProUGUI Text;
    private bool IsDraw = false;

    public GameObject stone;

    private void Update()
    {
        if (MultiTouchManager.instance.primary != int.MinValue)
        {
            Text.text = Input.GetTouch(MultiTouchManager.instance.primary).position.ToString();
            DrawLine(Input.GetTouch(MultiTouchManager.instance.primary).position);
        }
    }

    private void DrawLine(Vector3 pos)
    {
        stone.transform.position = pos;
    }
}
