using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables; // Import PlayableDirector

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene to load
    public GameObject loadingScreen; // UI element for loading screen
    public Slider loadingBar; // Slider to show loading progress
    public GameObject[] uiToHide; // Array of other UI elements to hide
    public PlayableDirector cutsceneDirector; // Reference to the PlayableDirector for the Timeline

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Hide other UI elements
        foreach (GameObject ui in uiToHide)
        {
            if (ui != null)
                ui.SetActive(false);
        }

        // Show loading screen
        if (loadingScreen != null)
            loadingScreen.SetActive(true);

        // Start loading the scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        // While the scene is loading
        while (!asyncOperation.isDone)
        {
            // Update loading progress
            if (loadingBar != null)
                loadingBar.value = asyncOperation.progress;

            // Activate the scene when loading is complete
            if (asyncOperation.progress >= 0.9f)
            {
                // Optionally wait for user input or a delay before activating
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        // Start the cutscene after the scene is loaded
        PlayCutscene();

        // Wait for the cutscene to finish before transitioning to the game
        yield return new WaitForSeconds((float)cutsceneDirector.duration); // Duration of the cutscene

        // Load the game level after cutscene is done
        SceneManager.LoadScene("LevelOne");
    }

    private void PlayCutscene()
    {
        // Check if the cutsceneDirector is assigned and then play the timeline
        if (cutsceneDirector != null)
        {
            cutsceneDirector.Play("CutsceneTest_Timeline"); // Start playing the cutscene Timeline
        }
    }
}
