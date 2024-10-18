using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 
Created By Joshua Guerrero
This script allows the player to
interact with an object using E
to switch scenes*/

public class SceneSwitcher : MonoBehaviour
{
    //references the SceneLoader script
    public SceneLoader sceneLoader;

    public TextMeshProUGUI interactionText;

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
                    sceneLoader.LoadScene("LevelTwo");
                }

                if (hit.collider.CompareTag("SwitchThree"))
                {
                    //loads LevelThree
                    Debug.Log("Scene is Switching");
                    sceneLoader.LoadScene("LevelThree");
                }
            }
        }

        else
        {
            // Hide the interaction text when not interacting
            interactionText.gameObject.SetActive(false);
        }

        // Perform a raycast to check for the interactable object
        RaycastHit hitInteractable;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInteractable, 3.75f)) // Distance is the same as interaction distance
        {
            // Show the interaction text if the object has the Scene Switch tags
            if (hitInteractable.collider.CompareTag("SwitchTwo") || hitInteractable.collider.CompareTag("SwitchThree"))
            {
                interactionText.text = "(E) Enter Truck"; // Set the interaction text
                interactionText.gameObject.SetActive(true); // Show the text
            }
            else
            {
                interactionText.gameObject.SetActive(false); // Hide the text if not interactable
            }
        }
        else
        {
            interactionText.gameObject.SetActive(false); // Hide the text if nothing is hit
        }
    }

}
