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
        mixer.SetFloat("masterVol", Mathf.Lerp(-80, 0, masterSlider.value));
    }

    public void ChangeMusic()
    {
        mixer.SetFloat("musicVol", Mathf.Lerp(-80, 0, musicSlider.value));
    }

    public void ChangeEffects()
    {
        mixer.SetFloat("sfxVol", Mathf.Lerp(-80, 0, effectsSlider.value));
    }
}
