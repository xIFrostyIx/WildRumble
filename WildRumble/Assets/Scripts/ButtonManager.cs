using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject optionsMenuUI;        // Options menu UI
    public Slider sfxVolumeSlider;          // Slider for adjusting button FX volume
    public Slider bgmVolumeSlider;          // Slider for adjusting BGM volume
    public btnFX buttonFXScript;            // Reference to the btnFX script (SFX sounds)
    public AudioSource bgmAudioSource;      // AudioSource for background music (BGM)

    void Start()
    {
        // Initialize SFX slider
        if (sfxVolumeSlider != null && buttonFXScript != null)
        {
            // Set slider value from saved preferences or default to 1 if none exists
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("ButtonFXVolume", 1f);
            buttonFXScript.SetVolume(sfxVolumeSlider.value); // Set initial volume for btnFX

            // Add listener to handle changes in slider value
            sfxVolumeSlider.onValueChanged.AddListener(SetButtonFXVolume);
        }

        // Initialize BGM slider
        if (bgmVolumeSlider != null && bgmAudioSource != null)
        {
            // Set slider value from saved preferences or default to 1 if none exists
            bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
            bgmAudioSource.volume = bgmVolumeSlider.value; // Set initial volume for BGM

            // Add listener to handle changes in slider value
            bgmVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
        }
    }

    // Method to set SFX volume from the slider
    public void SetButtonFXVolume(float volume)
    {
        // Set the volume in the btnFX script
        buttonFXScript.SetVolume(volume);

        // Save the current volume to PlayerPrefs
        PlayerPrefs.SetFloat("ButtonFXVolume", volume);
    }

    // Method to set BGM volume from the slider
    public void SetBGMVolume(float volume)
    {
        // Set the volume of the BGM AudioSource
        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = volume;
        }

        // Save the current BGM volume to PlayerPrefs
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
