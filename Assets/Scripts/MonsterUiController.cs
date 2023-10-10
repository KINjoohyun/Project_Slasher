using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUiController : MonoBehaviour
{
    public GameObject[] icons;
    public GameObject panel;
    private Queue<GameObject> queue = new Queue<GameObject>();
    private GameObject curObj = null;
    public ParticleSystem hitParticle;

    public void EnqueueImage(Pattern p)
    {
        switch (p)
        {
            case Pattern.Vertical:
                curObj = Instantiate(icons[0]);
                break;
            case Pattern.Horizontal:
                curObj = Instantiate(icons[1]);
                break;
            case Pattern.V:
                curObj = Instantiate(icons[2]);
                break;
            case Pattern.Caret:
                curObj = Instantiate(icons[3]);
                break;
            default:
                curObj = null;
                break;
        }
        curObj.transform.SetParent(panel.transform, false);
        if (curObj != null)
        { 
            queue.Enqueue(curObj);
        }
    }

    public void DequeueImage()
    {
        Destroy(queue.Peek());
        queue.Dequeue();

        hitParticle.Stop();
        hitParticle.Play();
    }

    public void Clear()
    {
        while (queue.Count > 0)
        {
            Destroy(queue.Peek());
            queue.Dequeue();
        }
    }
}
