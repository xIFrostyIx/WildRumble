using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject optionsMenuUI;
    public Slider sfxVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider gunShotSoundSlider; // New slider for gunshot sound
    public btnFX buttonFXScript;
    public AudioSource bgmAudioSource;

    void Start()
    {
        // SFX Volume Slider
        if (sfxVolumeSlider != null && buttonFXScript != null)
        {
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("ButtonFXVolume", 1f);
            buttonFXScript.SetVolume(sfxVolumeSlider.value);
            sfxVolumeSlider.onValueChanged.AddListener(SetButtonFXVolume);
        }

        // BGM Volume Slider
        if (bgmVolumeSlider != null && bgmAudioSource != null)
        {
            bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
            bgmAudioSource.volume = bgmVolumeSlider.value;
            bgmVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
        }

        // Gunshot Sound Volume Slider
        if (gunShotSoundSlider != null)
        {
            gunShotSoundSlider.value = PlayerPrefs.GetFloat("GunShotVolume", 1f); // Load previous volume setting
            gunShotSoundSlider.onValueChanged.AddListener(SetGunShotVolume);
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

    public void SetGunShotVolume(float volume) // New method for gunshot sound volume
    {
        // Assuming you have a reference to the Weapon script on the player or weapon object
        Weapon weaponScript = FindObjectOfType<Weapon>(); // Adjust this to find your weapon script correctly
        if (weaponScript != null)
        {
            weaponScript.gunShotVolume = volume; // Set the gunshot volume in the Weapon script
            PlayerPrefs.SetFloat("GunShotVolume", volume); // Save the new volume setting
        }
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
