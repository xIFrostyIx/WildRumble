using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject optionsMenuUI; // Reference to the options menu UI
    public Slider volumeSlider; // Reference to the volume slider

    void Start()
    {
        if (volumeSlider != null)
        {
            // Load the saved volume setting
            volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
            AudioListener.volume = volumeSlider.value;

            // Add listener to the volume slider
            volumeSlider.onValueChanged.AddListener(SetVolume);
       
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

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume; // Adjust the global audio volume
        PlayerPrefs.SetFloat("Volume", volume); // Save the volume setting
    }
}
