using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Optional for loading UI

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene to load
    public GameObject loadingScreen; // UI element for loading screen
    public Slider loadingBar; // a slider to show loading progress

    public void LoadScene()
    {
        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Show loading screen
        if (loadingScreen != null)
            loadingScreen.SetActive(true);

        // Start loading the scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        
        // Allow the scene to be loaded in the background
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

        // Hide loading screen
        if (loadingScreen != null)
            loadingScreen.SetActive(false);
    }
}
