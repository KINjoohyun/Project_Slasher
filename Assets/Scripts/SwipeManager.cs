using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeManager : MonoBehaviour
{
    private LineRenderer curLine;
    private Camera cam;
    private int positionCount = 2;
    private Vector3 prevPos = Vector3.zero;

    public float thick = 0.1f; // 선의 굵기
    public Material mat; // 메테리얼
    public Color startColor; // 선의 시작 색깔
    public Color endColor; // 선의 끝 색깔

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        DrawingUpdate();
    }

    void DrawingUpdate()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));

        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing(mousePos);
        }
        else if (Input.GetMouseButton(0))
        {
            ConnectDrawing(mousePos);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            LineCheck();
        }
    }

    void StartDrawing(Vector3 mousePos)
    {
        GameObject line = new GameObject("Line");
        LineRenderer lineRend = line.AddComponent<LineRenderer>();

        line.transform.parent = cam.transform;
        line.transform.position = mousePos;

        lineRend.startWidth = thick;
        lineRend.endWidth = thick;
        lineRend.material = mat;
        lineRend.startColor = startColor;
        lineRend.endColor = endColor;
        lineRend.numCornerVertices = 5;
        lineRend.numCapVertices = 5;
        lineRend.SetPosition(0, mousePos);
        lineRend.SetPosition(1, mousePos);

        curLine = lineRend;
    }

    void ConnectDrawing(Vector3 mousePos)
    {
        if (Vector3.Distance(prevPos, mousePos) >= 0.001f)
        {
            prevPos = mousePos;
            positionCount++;
            curLine.positionCount = positionCount;
            curLine.SetPosition(positionCount - 1, mousePos);
        }
    }

    void LineCheck()
    {
        GameManager.instance.HitMonsters('a'); // test code

        DeleteLine();
    }

    void DeleteLine()
    {
        positionCount = 2;
        prevPos = Vector3.zero;
        Destroy(curLine.gameObject);
        curLine = null;
    }
}
