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
    public Material mat; // ���׸���
    public Color startColor; // ���� ���� ����
    public Color endColor; // ���� �� ����
    public float similarity = 50.0f; // ��Ȯ��

    public Pattern testInput = Pattern.Vertical; // ���� �׽�Ʈ���� ����ó �Է�

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        DrawingUpdate();
    }

    private void DrawingUpdate()
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

    private void StartDrawing(Vector3 mousePos)
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

    private void ConnectDrawing(Vector3 mousePos)
    {
        if (Vector3.Distance(prevPos, mousePos) >= 0.001f)
        {
            prevPos = mousePos;
            positionCount++;
            curLine.positionCount = positionCount;
            curLine.SetPosition(positionCount - 1, mousePos);
        }
    }

    private void LineCheck()
    {
        //PixelReader(Pattern.Vertical);
        //LineCompare();
        testInput = GetPattern();
        GameManager.instance.HitMonsters(testInput);
        Debug.Log(testInput);

        DeleteLine();
    }

    private void DeleteLine()
    {
        positionCount = 2;
        prevPos = Vector3.zero;
        Destroy(curLine.gameObject);
        curLine = null;
    }

    private bool PixelReader(Pattern p)
    {
        Texture2D image;
        switch (p)
        {
            case Pattern.Vertical:
                image = (Texture2D)Resources.Load("Patterns/test");
                break;
            case Pattern.Horizontal:
                image = (Texture2D)Resources.Load("Patterns/test");
                break;
            default:
                image = null;
                break;
        }

        if (image == null)
        {
            Debug.LogWarning("Not Exist Image!");
            return false;
        }

        float similarityScore = 0.0f;
        float totalDifference = 0.0f;
        for (int i = 0; i < image.width; i++)
        {
            for (int j = 0; j < image.height; j++)
            {
                Color pixel = image.GetPixel(i, j);

                if (pixel.r <= Color.black.r && pixel.g <= Color.black.g && pixel.b <= Color.black.b)
                {
                    Debug.Log($"{i} , {j}"); // �ȼ� Get �Ϸ�
                }
            }
        }

        if (similarityScore >= similarity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Pattern GetPattern()
    {
        if (curLine.bounds.size.x > curLine.bounds.size.y * 1.25f)
        {
            return Pattern.Vertical;
        }
        else if (curLine.bounds.size.y > curLine.bounds.size.x * 1.25f)
        {
            return Pattern.Horizontal;
        }
        else
            return Pattern.None;
    }

}
