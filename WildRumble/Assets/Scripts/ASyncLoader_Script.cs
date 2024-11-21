using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ASyncLoader : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;  
    [SerializeField] private GameObject MainMenu;       

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;      

    
    public void LoadLevelBtn(string levelToLoad)
    {
        
        MainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        
        StartCoroutine(LoadLevelAsync(levelToLoad));
    }

   
    IEnumerator LoadLevelAsync(string levelToLoad)
    {
       
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

       
        loadOperation.allowSceneActivation = false;

       
        float loadTimer = 0f;
        float requiredLoadTime = 10f; 

        
        while (!loadOperation.isDone)
        {
            
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);  
            loadingSlider.value = progressValue;

            
            loadTimer += Time.deltaTime;

            
            if (loadTimer >= requiredLoadTime && loadOperation.progress >= 0.9f)
            {
                loadingSlider.value = 1f;  
                loadOperation.allowSceneActivation = true;  
            }

            
            yield return null;
        }
    }
}