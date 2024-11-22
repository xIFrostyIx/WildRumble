using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject losePanel;
    public btnFX buttonFX;

    public GameObject crosshairUI; 

    
    public static bool isPausedGlobal = false;

    void Update()
    {
        if (HealthBar.isGameOver)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayClickSound();
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (losePanel != null && losePanel.activeSelf)
            return;

        isPaused = !isPaused;
        isPausedGlobal = isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        isPausedGlobal = false;
        Time.timeScale = 1f;
        ShowCursor(false);
        pauseMenuUI.SetActive(false);
        ToggleCrosshair(true); 

        ToggleAudioSources(true);
        ToggleParticleSystems(true);
        Physics.autoSimulation = true; 
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        ShowCursor(true);
        pauseMenuUI.SetActive(true);
        ToggleCrosshair(false); 

        ToggleAudioSources(false);
        ToggleParticleSystems(false);
        Physics.autoSimulation = false; 
    }

    private void ToggleCrosshair(bool isActive)
    {
        if (crosshairUI != null)
        {
            crosshairUI.SetActive(isActive);
        }
    }

    private void ToggleAudioSources(bool enable)
    {
        foreach (AudioSource audio in FindObjectsOfType<AudioSource>())
        {
            if (enable)
                audio.UnPause();
            else
                audio.Pause();
        }
    }

    private void ToggleParticleSystems(bool enable)
    {
        foreach (ParticleSystem ps in FindObjectsOfType<ParticleSystem>())
        {
            if (enable)
                ps.Play();
            else
                ps.Pause();
        }
    }

    
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        isPausedGlobal = false;
        HealthBar.isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
    private void PlayClickSound()
    {
        if (buttonFX != null)
            buttonFX.ClickSound();
    }

    
    private void ShowCursor(bool show)
    {
        Cursor.lockState = show ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = show;
    }

    
    public void OpenOptions()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    
    public void BackToPauseMenu()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        isPausedGlobal = false;
        HealthBar.isGameOver = false;
        SceneManager.LoadScene("MainMenu"); 
    }
}
