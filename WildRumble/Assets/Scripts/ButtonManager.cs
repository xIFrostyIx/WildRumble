using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject optionsMenuUI;       
    public Slider sfxVolumeSlider;          
    public Slider bgmVolumeSlider;          
    public btnFX buttonFXScript;            
    public AudioSource bgmAudioSource;      

    void Start()
    {
        // 
        if (sfxVolumeSlider != null && buttonFXScript != null)
        {
           
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("ButtonFXVolume", 1f);
            buttonFXScript.SetVolume(sfxVolumeSlider.value); 

           
            sfxVolumeSlider.onValueChanged.AddListener(SetButtonFXVolume);
        }

        
        if (bgmVolumeSlider != null && bgmAudioSource != null)
        {
           
            bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
            bgmAudioSource.volume = bgmVolumeSlider.value; 

            
            bgmVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
        }
    }


    public void SetButtonFXVolume(float volume)
    {
    
        buttonFXScript.SetVolume(volume);

       
        PlayerPrefs.SetFloat("ButtonFXVolume", volume);
    }

    
    public void SetBGMVolume(float volume)
    {
        
        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = volume;
        }

       
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Play()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        if (optionsMenuUI != null)
        {
            optionsMenuUI.SetActive(true);
        }
    }

    public void CloseOptions()
    {
        if (optionsMenuUI != null)
        {
            optionsMenuUI.SetActive(false);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
