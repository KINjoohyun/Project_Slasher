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
}
