using UnityEngine;
using UnityEngine.UI;
//Made by Darcy
public class Amb : MonoBehaviour
{
    public Slider ambVolumeSlider;
    public AudioSource ambAudioSource;
    public PlayerMovement playerMovement;

    void Start()
    {
        if (ambVolumeSlider != null && ambAudioSource != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("AmbienceVolume", 1f);
            ambVolumeSlider.value = savedVolume;
            ambAudioSource.volume = savedVolume;
            ambVolumeSlider.onValueChanged.AddListener(SetAmbienceVolume);

            Debug.Log("Ambience volume initialized to: " + savedVolume);
        }
        else
        {
            Debug.LogWarning("ambVolumeSlider or ambAudioSource is not assigned in the Inspector.");
        }
    }

    public void SetAmbienceVolume(float volume)
    {
        if (ambAudioSource != null)
        {
            ambAudioSource.volume = volume;
            Debug.Log("Ambience volume set to: " + volume);
        }
        else
        {
            Debug.LogWarning("ambAudioSource is not set.");
        }

        PlayerPrefs.SetFloat("AmbienceVolume", volume);

        if (playerMovement != null)
        {
            playerMovement.SetFootstepVolume(volume);
            Debug.Log("Footstep volume also set to: " + volume);
        }
    }
}
