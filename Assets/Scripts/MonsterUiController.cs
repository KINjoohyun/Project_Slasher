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
    private GameObject inputObj = null;
    public ParticleSystem hitParticle;

    public void EnqueueImage(Pattern p)
    {
        switch (p)
        {
            case Pattern.Vertical:
                inputObj = Instantiate(icons[0]);
                break;
            case Pattern.Horizontal:
                inputObj = Instantiate(icons[1]);
                break;
            case Pattern.V:
                inputObj = Instantiate(icons[2]);
                break;
            case Pattern.Caret:
                inputObj = Instantiate(icons[3]);
                break;
            default:
                inputObj = null;
                break;
        }
        inputObj.transform.SetParent(panel.transform, false);
        if (inputObj != null)
        { 
            queue.Enqueue(inputObj);
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

    public bool IsEmpty()
    {
        return queue.Count <= 0;
    }
}
