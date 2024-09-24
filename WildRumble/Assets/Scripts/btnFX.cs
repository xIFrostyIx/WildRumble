using UnityEngine;

public class btnFX : MonoBehaviour
{
    public AudioSource myFx;               // AudioSource for button sound effects
    public AudioClip hoverFx;              // Sound for hover effect
    public AudioClip clickFx;              // Sound for click effect

    void Start()
    {
        // Initialize the volume from PlayerPrefs
        myFx.volume = PlayerPrefs.GetFloat("ButtonFXVolume", 1f);
    }

    // Method to set the volume for the button FX
    public void SetVolume(float volume)
    {
        if (myFx != null)
        {
            myFx.volume = volume;   // Adjust the volume of the button FX
        }
    }

    // Play hover sound
    public void HoverSound()
    {
        if (myFx != null && hoverFx != null)
        {
            myFx.PlayOneShot(hoverFx);
        }
    }

    // Play click sound
    public void ClickSound()
    {
        if (myFx != null && clickFx != null)
        {
            myFx.PlayOneShot(clickFx);
        }
    }
}
