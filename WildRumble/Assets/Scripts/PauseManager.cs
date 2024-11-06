using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject losePanel; 
    public btnFX buttonFX;
    //made by Darcy
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
        {
            return; 
        }

        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        ShowCursor(isPaused);
        pauseMenuUI.SetActive(isPaused);
        optionsMenuUI.SetActive(false);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        ShowCursor(false);
        pauseMenuUI.SetActive(false);
    }

    public void RestartLevel()
    {
        
        Time.timeScale = 1f;
        HealthBar.isGameOver = false;

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        HealthBar.isGameOver = false;

        
        SceneManager.LoadScene("MainMenu");

        
        Debug.Log("Returning to Main Menu and resetting game state");
    }


    private void PlayClickSound()
    {
        if (buttonFX != null)
        {
            buttonFX.ClickSound();
        }
    }

    private void ShowCursor(bool show)
    {
        Cursor.lockState = show ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = show;
    }
}
