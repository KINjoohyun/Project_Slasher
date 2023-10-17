using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMobject : MonoBehaviour
{
    public static BGMobject instance;
    private AudioSource sound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        sound = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void Stop()
    {
        sound.Stop();
    }

    public void Play()
    {
        sound.Play();
    }
}
