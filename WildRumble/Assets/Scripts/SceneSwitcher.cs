using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 
Created By Joshua Guerrero
This script allows the player to
interact with an object using E
to switch scenes*/

public class SceneSwitcher : MonoBehaviour
{
    private void Update()
    {
        // Checks if the player is pressing E
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Is Pressing E");
            RaycastHit hit;
            //Player has to be a certain distance and looking at the object
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3.75f))
            {
                //checks if prop has a "Switch" tag
                if (hit.collider.CompareTag("SwitchTwo"))
                {
                    //loads LevelTwo
                    Debug.Log("Scene is Switching");
                    SceneManager.LoadScene("LevelTwo");
                }

                if (hit.collider.CompareTag("SwitchThree"))
                {
                    //loads LevelThree
                    Debug.Log("Scene is Switching");
                    SceneManager.LoadScene("LevelThree");
                }
            }
        }
    }
}
