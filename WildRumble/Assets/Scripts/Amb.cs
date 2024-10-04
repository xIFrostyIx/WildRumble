using UnityEngine;
using UnityEngine.UI;

public class Amb : MonoBehaviour
{
    public Slider ambVolumeSlider;          
    public AudioSource ambAudioSource;     

    void Start()
    {
       
        if (ambVolumeSlider != null && ambAudioSource != null)
        {
            ambVolumeSlider.value = PlayerPrefs.GetFloat("AmbienceVolume", 1f);
            ambAudioSource.volume = ambVolumeSlider.value;

            
            ambVolumeSlider.onValueChanged.AddListener(SetAmbienceVolume);
        }
    }

    
    public void SetAmbienceVolume(float volume)
    {
        if (ambAudioSource != null)
        {
            ambAudioSource.volume = volume;
        }

        
        PlayerPrefs.SetFloat("AmbienceVolume", volume);
    }
}
