using TMPro;
using UnityEngine;

public class InputTester : MonoBehaviour
{
    private Camera maincam;
    public TextMeshProUGUI patternText;
    public TextMeshProUGUI boundText;
    private bool IsDraw = false;

    public GameObject stone;
    private TrailRenderer trail;
    private Pattern p = Pattern.None;

    private void Awake()
    {
        maincam = Camera.main;
        trail = stone.GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (MultiTouchManager.instance.primary != int.MinValue)
        {
            IsDraw = true;

            var prime = Input.GetTouch(MultiTouchManager.instance.primary).position;
            Vector3 pos = maincam.ScreenToWorldPoint(new Vector3(prime.x, prime.y, 10));

            DrawLine(pos);
        }
        else if (IsDraw)
        {
            IsDraw = false;

            CheckLine();
            DeleteLine();
            PrintPattern();
        }
    }

    private void DrawLine(Vector3 pos)
    {
        stone.SetActive(true);
        stone.transform.position = pos;
    }

    private void DeleteLine()
    {
        trail.Clear();
        stone.transform.position = Vector3.zero;
        stone.SetActive(false);
    }

    private void CheckLine()
    {
        var size = trail.bounds.size;
        boundText.text = size.ToString();

        // Horizontal
        if (size.z <= 1.0f && size.x > size.z && size.x >= 0.5f)
        {
            p = Pattern.Horizontal;
        }
        // Vertical
        else if (size.x <= 1.0f && size.z > size.x && size.z >= 0.5f)
        {
            p = Pattern.Vertical;
        }
        // V
        else if (trail.bounds.ClosestPoint(trail.GetPosition(0)).z < trail.bounds.center.z &&
            trail.bounds.ClosestPoint(trail.GetPosition(trail.positionCount - 1)).z < trail.bounds.center.z &&
            trail.bounds.ClosestPoint(trail.GetPosition(trail.positionCount / 2)).z > trail.bounds.center.z)
        {
            p = Pattern.V;
        }
        // Caret
        else if (trail.bounds.ClosestPoint(trail.GetPosition(0)).z > trail.bounds.center.z &&
            trail.bounds.ClosestPoint(trail.GetPosition(trail.positionCount - 1)).z > trail.bounds.center.z &&
            trail.bounds.ClosestPoint(trail.GetPosition(trail.positionCount / 2)).z < trail.bounds.center.z)
        {
            p = Pattern.Caret;
        }
        // None
        else
        {
            p = Pattern.None;
        }
        
    }

    private void PrintPattern()
    {
        patternText.text = p.ToString();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (trail != null)
        {
            Gizmos.DrawWireCube(trail.bounds.center, trail.bounds.size);
        }
    }
}
