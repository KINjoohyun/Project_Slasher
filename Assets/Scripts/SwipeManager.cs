using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public static SwipeManager instance;

    private Camera maincam;
    public GameObject stone;
    private TrailRenderer trail;
    public bool IsDraw { get; private set; }

    private Pattern swipeInput = Pattern.None;
    public ParticleSystem slashParticle;
    private AudioSource sound;
    public AudioClip slashSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        maincam = Camera.main;
        trail = stone.GetComponent<TrailRenderer>();
        sound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (GameManager.instance.IsGameover || GameManager.instance.IsPause)
        {
            return;
        }
        DrawingUpdate();
    }

    private void DrawingUpdate()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButton(0))
        {
            IsDraw = true;

            Vector3 pos = maincam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

            DrawLine(pos);
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            LineCheck();
            DeleteLine();
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (MultiTouchManager.instance.primary != int.MinValue)
        {
            IsDraw = true;

            var prime = Input.GetTouch(MultiTouchManager.instance.primary).position;
            Vector3 pos = maincam.ScreenToWorldPoint(new Vector3(prime.x, prime.y, 10));

            DrawLine(pos);
        }
        else if (IsDraw)
        {
            LineCheck();
            DeleteLine();
        }
#endif
    }

    private void DrawLine(Vector3 pos)
    {
        stone.SetActive(IsDraw);
        stone.transform.position = pos;
    }

    private void LineCheck()
    {
        if (trail.positionCount <= 0)
        {
            return;
        }

        // Horizontal
        if (IsHorizontal())
        {
            swipeInput = Pattern.Horizontal;
        }
        // Vertical
        else if (IsVertical())
        {
            swipeInput = Pattern.Vertical;
        }
        // V
        else if (IsV())
        {
            swipeInput = Pattern.V;
        }
        // Caret
        else if (IsCaret())
        {
            swipeInput = Pattern.Caret;
        }
        // None
        else
        {
            swipeInput = Pattern.None;

            return;
        }

        GameManager.instance.SlashMonsters(swipeInput);
        slashParticle.Stop();
        slashParticle.Play();
        sound.PlayOneShot(slashSound);

        DeleteLine();
    }

    private bool IsHorizontal()
    {
        var size = trail.bounds.size;
        return size.z <= 1.0f && size.x > size.z && size.x >= 0.5f;
    }

    private bool IsVertical()
    {
        var size = trail.bounds.size;
        return size.x <= 1.0f && size.z > size.x && size.z >= 0.5f;
    }

    private bool IsV()
    {
        return trail.bounds.ClosestPoint(trail.GetPosition(0)).z < trail.bounds.center.z &&
            trail.bounds.ClosestPoint(trail.GetPosition(trail.positionCount - 1)).z < trail.bounds.center.z &&
            trail.bounds.ClosestPoint(trail.GetPosition(trail.positionCount / 2)).z > trail.bounds.center.z;
    }

    private bool IsCaret()
    {
        return trail.bounds.ClosestPoint(trail.GetPosition(0)).z > trail.bounds.center.z &&
            trail.bounds.ClosestPoint(trail.GetPosition(trail.positionCount - 1)).z > trail.bounds.center.z &&
            trail.bounds.ClosestPoint(trail.GetPosition(trail.positionCount / 2)).z < trail.bounds.center.z;
    }

    public void DeleteLine()
    {
        IsDraw = false;

        trail.Clear();
        stone.transform.position = Vector3.zero;
        stone.SetActive(IsDraw);
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
