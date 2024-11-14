using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Created by Joshua Guerrero
 * This script handles the list of
 * objectives and their completion logic
 */

public class ObjectiveManager : MonoBehaviour
{
    public List<Objective> objectives = new List<Objective>();
    public TextMeshProUGUI objectiveText;

    // Truck Icon on the mini-map
    public GameObject truckMiniMapIcon; // Assign the truck icon in the inspector

    private int currentObjectiveIndex = 0; // Keeps track of the current objective

    private void Start()
    {
        // Initialize objectives
        objectives.Add(new Objective("Eliminate 5 Animals", 5));
        objectives.Add(new Objective("Get in the truck", 1));  // This objective will be shown only after the first is complete

        UpdateObjectiveText();

        // Initially hide the truck icon on the mini-map
        if (truckMiniMapIcon != null)
        {
            truckMiniMapIcon.SetActive(false);
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

                // If objective is complete, update to the next objective
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
        // Show only the current objective
        var currentObjective = objectives[currentObjectiveIndex];
        objectiveText.text = $"{currentObjective.description}: {currentObjective.currentCount}/{currentObjective.targetCount} - {(currentObjective.isCompleted ? "Completed" : "In Progress")}";

        // If the current objective is "Get in the truck", show the truck mini-map icon
        if (currentObjective.description == "Get in the truck" && !currentObjective.isCompleted)
        {
            if (truckMiniMapIcon != null)
            {
                truckMiniMapIcon.SetActive(true);  // Show truck icon on mini-map
            }
        }
        else if (truckMiniMapIcon != null)
        {
            truckMiniMapIcon.SetActive(false);  // Hide truck icon when not needed
        }
    }

    public bool IsObjectiveComplete(string description)
    {
        var objective = objectives.Find(o => o.description == description);
        return objective != null && objective.isCompleted;
    }
}
