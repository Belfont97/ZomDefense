using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
    }

    public void setLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20); // set volume using log and slider value (log makes slider changes make sense; half of slider is half volume)
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }
}
