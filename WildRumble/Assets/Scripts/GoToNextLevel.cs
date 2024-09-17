using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextLevel : MonoBehaviour
{
    public void loadTheLevel(int buildNum)
    {
        Debug.Log("NextLevel");
        SceneManager.LoadScene(buildNum);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("QuitQuit");
            Application.Quit(); // Quits the game
        }


    }
}