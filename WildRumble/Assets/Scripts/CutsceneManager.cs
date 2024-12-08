using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by Joshua Guerrero
 * This script references the Scene Loader 
 * script tp allow async loading upon levels and
 * cut scenes, and also switches scenes when the cut scene
 * is over
 */

/*
 * HOW TO USE:
 * In a cutscene scene
 * Make and empty game object called "Cutscene Manager"
 * attach this script to "Cutscene Manager" game object
 * Make an empty game object called "Scene Loader"
 * attach the scene loader script to it
 * Drag the "Scene Loader" game object into the 
 * proper space in the "Cutscene Manager" game object
 * adjust time in the "Cutscene Manager" to match length
 * of the cutscene
 */

public class CutsceneManager : MonoBehaviour
{
    public SceneLoader sceneLoader; // Reference to SceneLoader script
    public float cutsceneDuration = 10f; // Duration of the cutscene in seconds

    private void Start()
    {
        // Automatically transition to the next scene after the cutscene duration
        Invoke(nameof(EndCutscene), cutsceneDuration);
    }

    public void EndCutscene()
    {
        Debug.Log("Cutscene finished. Loading the next scene.");
        if (sceneLoader != null)
        {
            sceneLoader.LoadNextSceneAfterCutscene();
        }
        else
        {
            Debug.LogError("SceneLoader reference is missing!");
        }
    }
}
