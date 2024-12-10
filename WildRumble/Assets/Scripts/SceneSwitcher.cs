using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneSwitcher : MonoBehaviour
{
    public SceneLoader sceneLoader; // Reference to the SceneLoader script
    public TextMeshProUGUI interactionText;

    // Reference to the ObjectiveManager to check objective status
    public ObjectiveManager objectiveManager;

    private void Update()
    {
        // Perform a raycast to check for the interactable object
        RaycastHit hitInteractable;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInteractable, 3.75f)) // Interaction distance
        {
            // Check if the player has completed the "Eliminate 5 Animals" objective
            if (objectiveManager.IsObjectiveComplete("Eliminate 5 Animals"))
            {
                Debug.Log("Objective 'Eliminate 5 Animals' is complete!");
                // Show the interaction text if the object has the Scene Switch tags
                if (hitInteractable.collider.CompareTag("SwitchTwo") || hitInteractable.collider.CompareTag("SwitchThree") || hitInteractable.collider.CompareTag("SwitchMain"))
                {
                    interactionText.text = "(E) Enter Truck"; // Set the interaction text
                    interactionText.gameObject.SetActive(true); // Show the text

                    // Handle the interaction when pressing E
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log($"Interacting with {hitInteractable.collider.tag}");
                        if (hitInteractable.collider.CompareTag("SwitchTwo"))
                        {
                            Debug.Log("Scene is Switching");
                            sceneLoader.LoadScene("Cutscene 2");
                        }
                        else if (hitInteractable.collider.CompareTag("SwitchThree"))
                        {
                            Debug.Log("Scene is Switching");
                            sceneLoader.LoadScene("Cutscene 3");
                        }
                        else if (hitInteractable.collider.CompareTag("SwitchMain"))
                        {
                            Debug.Log("Scene is Switching");
                            sceneLoader.LoadScene("Cutscene 4");
                            //sceneLoader.LoadScene("MainMenu");
                            //Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSeQ-pE-3Fk9g7x_3E20kTP95STGiwg681mYpJRIM9yRPZ2LJQ/viewform?usp=sf_link");
                            //Application.Quit();
                        }
                    }
                }
                else
                {
                    Debug.Log("Objective 'Eliminate 5 Animals' is not complete!");
                    interactionText.gameObject.SetActive(false); // Hide the text if not interactable
                }
            }
            else
            {
                interactionText.gameObject.SetActive(false); // Hide the text if objective is not completed
            }
        }
        else
        {
            interactionText.gameObject.SetActive(false); // Hide the text if nothing is hit
        }
    }
}
