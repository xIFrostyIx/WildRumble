using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Created by Joshua Guerrero
 * This script allows the player to
 * interact with the truck using E
 * to switch scenes
 */

public class SceneSwitcher : MonoBehaviour
{
    // Reference to the SceneLoader script
    public SceneLoader sceneLoader;
    public TextMeshProUGUI interactionText;
    public ObjectiveManager objectiveManager;

    private void Update()
    {
        // Disable interaction with the truck until the first objective is completed
        if (!objectiveManager.IsObjectiveComplete("Eliminate 5 Animals"))
        {
            interactionText.gameObject.SetActive(false);
            return;
        }

        // Check if the player is pressing E and is close to an interactable object
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3.75f))
            {
                if (hit.collider.CompareTag("SwitchTwo"))
                {
                    sceneLoader.LoadScene("LevelTwo");
                }

                if (hit.collider.CompareTag("SwitchThree"))
                {
                    sceneLoader.LoadScene("LevelThree");
                }

                if (hit.collider.CompareTag("SwitchMain"))
                {
                    sceneLoader.LoadScene("MainMenu");
                }
            }
        }

        // Perform a raycast to check for the interactable object
        RaycastHit hitInteractable;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInteractable, 3.75f))
        {
            if (hitInteractable.collider.CompareTag("SwitchTwo") || hitInteractable.collider.CompareTag("SwitchThree") || hitInteractable.collider.CompareTag("SwitchMain"))
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
