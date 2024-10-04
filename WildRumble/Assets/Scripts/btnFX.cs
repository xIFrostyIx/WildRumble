using UnityEngine;

public class btnFX : MonoBehaviour
{
    public AudioSource myFx;               
    public AudioClip hoverFx;              
    public AudioClip clickFx;              
    void Start()
    {
        
        myFx.volume = PlayerPrefs.GetFloat("ButtonFXVolume", 1f);
    }

    
    public void SetVolume(float volume)
    {
        if (myFx != null)
        {
            myFx.volume = volume;   
        }
    }

    
    public void HoverSound()
    {
        if (myFx != null && hoverFx != null)
        {
            myFx.PlayOneShot(hoverFx);
        }
    }

    
    public void ClickSound()
    {
        if (myFx != null && clickFx != null)
        {
            myFx.PlayOneShot(clickFx);
        }
    }
}
