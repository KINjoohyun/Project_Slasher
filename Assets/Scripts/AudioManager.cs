using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider effectsSlider;

    private void Awake()
    {
        PlayDataManager.Init();
    }

    private void Start()
    {
        masterSlider.value = PlayDataManager.data.masterVol;
        musicSlider.value = PlayDataManager.data.musicVol;
        effectsSlider.value = PlayDataManager.data.sfxVol;

        ChangerMaster();
        ChangeMusic();
        ChangeEffects();
    }

    public void ChangerMaster()
    {
        mixer.SetFloat("masterVol", Mathf.Log10(masterSlider.value) * 20);
    }

    public void ChangeMusic()
    {
        mixer.SetFloat("musicVol", Mathf.Log10(musicSlider.value) * 20);
    }

    public void ChangeEffects()
    {
        mixer.SetFloat("sfxVol", Mathf.Log10(effectsSlider.value) * 20);
    }

    public void Save()
    {
        PlayDataManager.data.masterVol = masterSlider.value;
        PlayDataManager.data.musicVol = musicSlider.value;
        PlayDataManager.data.sfxVol = effectsSlider.value;

        PlayDataManager.Save();
    }
}
