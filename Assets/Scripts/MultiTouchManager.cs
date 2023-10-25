using System;
using System.Collections.Generic;
using UnityEngine;

public class MultiTouchManager : MonoBehaviour
{
    public static MultiTouchManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public bool IsTouching { get; private set; }
    public float ZoomInch { get; private set; }
    
    private List<int> fingerIdList = new List<int>();

    public int primary = int.MinValue;

    private void Update()
    {
        foreach (var touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (fingerIdList.Count == 0 && primary == int.MinValue)
                    {
                        primary = touch.fingerId;
                    }
                    fingerIdList.Add(touch.fingerId);
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (touch.fingerId == primary)
                    {
                        primary = int.MinValue;
                    }
                    fingerIdList.Remove(touch.fingerId);
                    break;
            }


        }

        
    }

    private void Zoom()
    {
        if (fingerIdList.Count >= 2)
        {
            Vector2[] prevPos = new Vector2[2];
            Vector2[] curPos = new Vector2[2];

            for (int i = 0; i < 2; i++)
            {
                var touch = Array.Find(Input.touches, x => x.fingerId == fingerIdList[i]);
                curPos[i] = touch.position;
                prevPos[i] = curPos[i] - touch.deltaPosition;
            }

            var prevDis = Vector2.Distance(prevPos[0], prevPos[1]);
            var curDis = Vector2.Distance(curPos[0], curPos[1]);
            var dis = curDis - prevDis;

            ZoomInch = dis;
            if (dis > 0)
            {
                Debug.Log("Zoom In");
            }
            else if (dis < 0)
            {
                Debug.Log("Zoom Out");
            }
        }
    }
}
