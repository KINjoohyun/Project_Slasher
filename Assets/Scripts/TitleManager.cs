using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class TitleManager : MonoBehaviour
{
    public static TitleManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public AudioMixer mixer;

    private void Start()
    {
        PlayDataManager.Init();

        mixer.SetFloat("masterVol", Mathf.Log10(PlayDataManager.data.masterVol) * 20);
        mixer.SetFloat("musicVol", Mathf.Log10(PlayDataManager.data.musicVol) * 20);
        mixer.SetFloat("sfxVol", Mathf.Log10(PlayDataManager.data.sfxVol) * 20);
    }

}