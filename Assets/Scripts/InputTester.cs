using TMPro;
using Unity.VisualScripting;
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

    public float rangeAngle = 10.0f;

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

        // None
        if (trail.positionCount <= 1)
        {
            p = Pattern.None;
        }
        // Horizontal
        else if (IsHorizontal())
        {
            p = Pattern.Horizontal;
        }
        // Vertical
        else if (IsVertical())
        {
            p = Pattern.Vertical;
        }
        // V
        else if (IsV())
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

    private bool IsHorizontal()
    {
        Vector3 normal = Vector3.right;
        for (int i = 0; i < trail.positionCount - 1; i++)
        {
            Vector3 vec = trail.GetPosition(i + 1) - trail.GetPosition(i);
            vec.Normalize();
            var ang = Vector3.Angle(vec, normal);

            if (ang > rangeAngle && ang < 180 - rangeAngle)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsVertical()
    {
        Vector3 normal = new Vector3(0, 0, 1);
        for (int i = 0; i < trail.positionCount - 1; i++)
        {
            Vector3 vec = trail.GetPosition(i + 1) - trail.GetPosition(i);
            vec.Normalize();
            var ang = Vector3.Angle(vec, normal);

            if (ang > rangeAngle && ang < 180 - rangeAngle)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsV()
    {
        {
            Vector3 normal = new Vector3(1, 0, 1);
            Vector3 vec = trail.GetPosition(trail.positionCount / 2) - trail.GetPosition(0);
            vec.Normalize();
            var ang = Vector3.Angle(vec, normal);

            Debug.Log(ang);

            if (ang > rangeAngle && ang < 180 - rangeAngle)
            {
                return false;
            }
        }
        {
            Vector3 normal = new Vector3(1, 0, -1);
            Vector3 vec = trail.GetPosition(trail.positionCount) - trail.GetPosition(trail.positionCount / 2);
            vec.Normalize();
            var ang = Vector3.Angle(vec, normal);

            Debug.Log(ang);

            if (ang > rangeAngle && ang < 180 - rangeAngle)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsCaret()
    {
        return false;
    }

    private void PrintPattern()
    {
        patternText.text = p.ToString();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (trail != null)
        {
            Gizmos.DrawWireCube(trail.bounds.center, trail.bounds.size);
        }
    }
#endif
}
