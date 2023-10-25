using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class InputTester : MonoBehaviour
{
    private Camera maincam;
    public TextMeshProUGUI Text;
    private bool IsDraw = false;

    public GameObject stone;

    private void Awake()
    {
        maincam = Camera.main;
    }

    private void Update()
    {
        if (MultiTouchManager.instance.primary != int.MinValue)
        {
            var prime = Input.GetTouch(MultiTouchManager.instance.primary).position;
            Vector3 pos = maincam.ScreenToWorldPoint(new Vector3(prime.x, prime.y, 1));

            DrawLine(pos);
        }
        else
        {
            DeleteLine();
        }
    }

    private void DrawLine(Vector3 pos)
    {
        stone.SetActive(true);
        stone.transform.position = pos;
    }

    private void DeleteLine()
    {
        stone.GetComponent<TrailRenderer>().Clear();
        stone.SetActive(false);
    }
}
