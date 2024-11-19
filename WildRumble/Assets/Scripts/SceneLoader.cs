using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene to load
    public GameObject loadingScreen; // UI element for loading screen
    public Slider loadingBar; // Slider to show loading progress
    public GameObject[] uiToHide; // Array of other UI elements to hide

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

        // Hide loading screen (optional, depending on transition style)
        if (loadingScreen != null)
            loadingScreen.SetActive(false);
    }
}
