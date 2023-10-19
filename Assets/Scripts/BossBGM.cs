using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBGM : MonoBehaviour
{
    public static BossBGM instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        sound = GetComponent<AudioSource>();
    }

    public AudioSource bgm;
    private AudioSource sound;

    public void Play()
    {
        bgm.Stop();
        sound.Play();
    }

    public void Stop()
    {
        sound.Stop();
        bgm.Play();
    }
}
