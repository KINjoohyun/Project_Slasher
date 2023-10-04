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
    public float similarity = 50.0f; // 정확도

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
        //PixelReader(Pattern.Vertical);
        //GameManager.instance.HitMonsters(Pattern.Vertical);
        LineCompare();

        DeleteLine();
    }

    void DeleteLine()
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
                    Debug.Log($"{i} , {j}"); // 픽셀 Get 완료
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

    void LineCompare()
    {
        Texture2D image = (Texture2D)Resources.Load("Patterns/test");
        if (image == null)
        {
            Debug.LogWarning("Not Exist Image!");
            return;
        }
        int width = image.width;
        int height = image.height;
        float totalDifference = 0;

        for (int i = 0; i < curLine.positionCount; i++)
        {
            Vector3 linePoint = curLine.GetPosition(i);
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(linePoint);

            int x = Mathf.Clamp(Mathf.RoundToInt(screenPoint.x), 0, width - 1);
            int y = Mathf.Clamp(Mathf.RoundToInt(screenPoint.y), 0, height - 1);

            Color imagePixel = image.GetPixel(x, y);

            float difference = Mathf.Sqrt(Mathf.Pow(imagePixel.r - Color.black.r, 2) + Mathf.Pow(imagePixel.g - Color.black.g, 2) + Mathf.Pow(imagePixel.b - Color.black.b, 2));
            totalDifference += difference;
        }

        float similarityScore = totalDifference / curLine.positionCount;
        Debug.Log(similarityScore);
        if (similarityScore >= similarity) GameManager.instance.HitMonsters(Pattern.Vertical);
    }

}
