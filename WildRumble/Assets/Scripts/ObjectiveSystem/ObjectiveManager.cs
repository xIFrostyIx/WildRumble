using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public List<Objective> objectives = new List<Objective>();
    public TextMeshProUGUI objectiveText;

    // Truck Icon on the mini-map
    public GameObject truckMiniMapIcon;
    public Transform truckTransform; // Assign the truck transform in the inspector
    public Transform playerTransform; // Assign the player transform in the inspector
    public float miniMapRadius = 50f; // Radius within which the icon will stay centered on mini-map

    private int currentObjectiveIndex = 0; // Keeps track of the current objective

    private void Start()
    {
        if (objectives.Count == 0)
        {
            objectives.Add(new Objective("Eliminate 5 Animals", 5));
            objectives.Add(new Objective("Get in the truck", 1));
        }

        UpdateObjectiveText();

        if (truckMiniMapIcon != null)
        {
            truckMiniMapIcon.SetActive(false);
        }
    }

    private void Update()
    {
        // Update truck icon visibility based on the "Get in the truck" objective
        if (objectives[currentObjectiveIndex].description == "Get in the truck" && !objectives[currentObjectiveIndex].isCompleted)
        {
            UpdateTruckIconPosition();
        }
    }

    public void UpdateObjective(string objectiveDescription)
    {
        foreach (var objective in objectives)
        {
            if (objective.description == objectiveDescription)
            {
                objective.currentCount++;
                objective.UpdateObjective();
                Debug.Log($"Objective {objective.description}: {objective.currentCount}/{objective.targetCount} - Completed: {objective.isCompleted}");

                if (objective.isCompleted && currentObjectiveIndex < objectives.Count - 1)
                {
                    currentObjectiveIndex++;
                }

                UpdateObjectiveText();
                break;
            }
        }
    }

    private void UpdateObjectiveText()
    {
        if(objectives.Count == 0 || currentObjectiveIndex >= objectives.Count)
        {
            Debug.LogError("Objective list is empty or index is out of bounds.");
            objectiveText.text = "No objectives available.";
            return;
        }

        var currentObjective = objectives[currentObjectiveIndex];
        objectiveText.text = $"{currentObjective.description}: {currentObjective.currentCount}/{currentObjective.targetCount} - {(currentObjective.isCompleted ? "Completed" : "In Progress")}";

        // Show truck icon only for the "Get in the truck" objective and if it's not completed
        if (currentObjective.description == "Get in the truck" && !currentObjective.isCompleted)
        {
            truckMiniMapIcon.SetActive(true);
        }
        else
        {
            truckMiniMapIcon.SetActive(false);
        }
    }

    private void UpdateTruckIconPosition()
    {
        if (truckMiniMapIcon == null || truckTransform == null || playerTransform == null) return;

        Vector3 truckPosition = truckTransform.position;
        Vector3 playerPosition = playerTransform.position;

        // Calculate the relative position of the truck from the player
        Vector3 relativePosition = truckPosition - playerPosition;

        // Keep the icon within the mini-map radius
        if (relativePosition.magnitude > miniMapRadius)
        {
            // Move icon to the edge of the mini-map in the direction of the truck
            relativePosition = relativePosition.normalized * miniMapRadius;
        }

        // Update the icon's position in the UI to reflect this position
        truckMiniMapIcon.transform.localPosition = new Vector3(relativePosition.x, relativePosition.z, 0);
    }

    public bool IsObjectiveComplete(string description)
    {
        var objective = objectives.Find(o => o.description == description);
        return objective != null && objective.isCompleted;
    }
}
