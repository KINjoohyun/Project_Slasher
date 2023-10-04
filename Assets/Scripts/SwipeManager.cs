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

    public float thick = 0.1f; // ���� ����

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
        lineRend.startColor = Color.red;
        lineRend.endColor = Color.red;
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
