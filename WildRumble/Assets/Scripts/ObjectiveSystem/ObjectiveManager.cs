using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

/*
 * Created by Joshua Guerrero
 * This script handles the lsit of
 * objectives and their completion logic
 */

public class ObjectiveManager : MonoBehaviour
{
    public List<Objective> objectives = new List<Objective>();
    public TextMeshProUGUI objectiveText;

    private void Start()
    {
        // Initialize objectives
        objectives.Add(new Objective("Eliminate 5 deer", 5));
        objectives.Add(new Objective("Get in the truck", 1));
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
                UpdateObjectiveText();
                break;
            }
        }
    }

    private void UpdateObjectiveText()
    {
        objectiveText.text = "";
        foreach (var objective in objectives)
        {
            objectiveText.text += $"{objective.description}: {objective.currentCount}/{objective.targetCount} - {(objective.isCompleted ? "Completed" : "Pending")}\n";
        }
    }
}
