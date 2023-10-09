using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SwipeManager : MonoBehaviour
{
    private LineRenderer curLine;
    public RenderTexture rendtex;
    private Camera maincam;
    private int positionCount = 2;
    private Vector3 prevPos = Vector3.zero;
    private Pattern testInput = Pattern.None;
    public ParticleSystem slashParticle;

    public float thick = 0.1f; // 선의 굵기
    public Material mat; // 메테리얼
    public Color startColor; // 선의 시작 색깔
    public Color endColor; // 선의 끝 색깔
    public float similarity = 50.0f; // 정확도

    private void Awake()
    {
        maincam = Camera.main;
    }

    private void Update()
    {
        DrawingUpdate();
    }

    private void DrawingUpdate()
    {
        Vector3 mousePos = maincam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));

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
        line.layer = LayerMask.NameToLayer("Line");
        LineRenderer lineRend = line.AddComponent<LineRenderer>();

        line.transform.parent = maincam.transform;
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
        testInput = Pattern.None;
        for (Pattern i = 0; i < Pattern.Count; i++)
        {
            if (PixelReader(i)) 
            {
                testInput = i;
                slashParticle.Stop();
                slashParticle.Play();
                break;
            }
        }
 
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
                image = (Texture2D)Resources.Load("Patterns/vertical");
                break;
            case Pattern.Horizontal:
                image = (Texture2D)Resources.Load("Patterns/horizontal");
                break;
            case Pattern.V:
                image = (Texture2D)Resources.Load("Patterns/v");
                break;
            case Pattern.Caret:
                image = (Texture2D)Resources.Load("Patterns/caret");
                break;
            default:
                image = null;
                break;
        }

        if (image == null) // Exception Handling
        {
            Debug.LogWarning("Not Exist Image!");
            return false;
        }


        RenderTexture.active = rendtex;
        Texture2D tex = new Texture2D(rendtex.width, rendtex.height);
        tex.ReadPixels(new Rect(0, 0, rendtex.width, rendtex.height), 0, 0);
        tex.Apply();

        float total = 0.0f;
        float result = 0.0f;
        for (int i = 0; i < image.width; i++)
        {
            for (int j = 0; j < image.height; j++)
            {
                Color pixel = image.GetPixel(i, j);
                Color texPixel = tex.GetPixel(i, j);

                if (pixel.CompareRGB(Color.black))
                {
                    total++;

                    if (!texPixel.CompareRGB(Color.white))
                    {
                        result++;
                    }
                }
            }
        }
        float similarityScore = result / total * 100.0f;
        RenderTexture.active = null;
        Destroy(tex);

        //Debug.Log($"{p} : {similarityScore}");
        if (similarityScore >= similarity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (curLine != null)
        {
            Gizmos.DrawWireCube(curLine.bounds.center, curLine.bounds.size);
        }
    }
    */
}
