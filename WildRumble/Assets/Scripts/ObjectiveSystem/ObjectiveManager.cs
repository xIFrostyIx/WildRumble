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

    private int currentObjectiveIndex = 0; // Keeps track of the current objective

    private void Start()
    {
        // Initialize objectives
        objectives.Add(new Objective("Eliminate 5 Animals", 5));
        objectives.Add(new Objective("Get in the truck", 1));  // This objective will be shown only after the first is complete

        UpdateObjectiveText();
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
        objectiveText.text = $"{objectives[currentObjectiveIndex].description}: {objectives[currentObjectiveIndex].currentCount}/{objectives[currentObjectiveIndex].targetCount} - {(objectives[currentObjectiveIndex].isCompleted ? "Completed" : "In Progress")}";
    }

    public bool IsObjectiveComplete(string description)
    {
        var objective = objectives.Find(o => o.description == description);
        return objective != null && objective.isCompleted;
    }
}
