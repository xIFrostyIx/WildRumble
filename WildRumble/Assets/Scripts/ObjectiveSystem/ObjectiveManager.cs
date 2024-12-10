using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Created By Joshua Guerrero
 * This script displays the objectives
 * and correctly updates them as completed
 * 
 * It also tracks objectives on the mini map
 */

public class ObjectiveManager : MonoBehaviour
{
    public List<Objective> objectives = new List<Objective>();
    public TextMeshProUGUI objectiveText;

    // Truck Icon on the mini-map
    public GameObject truckMiniMapIcon;
    public Transform truckTransform; // Assign the truck transform in the inspector

    // Zoo Keeper Icon on the mini-map
    public GameObject zooKeeperMiniMapIcon;
    public Transform zooKeeperTransform; // Assign the Zoo Keeper's transform in the inspector

    public Transform playerTransform; // Assign the player transform in the inspector
    public float miniMapRadius = 50f; // Radius within which the icon will stay centered on mini-map

    private int currentObjectiveIndex = 0; // Keeps track of the current objective
    public int level = 1; // Set the level in the inspector or via script

    private void Start()
    {
        InitializeObjectivesForLevel();
        UpdateObjectiveText();

        // Initialize mini-map icons
        if (truckMiniMapIcon != null)
        {
            truckMiniMapIcon.SetActive(false);
        }
        if (zooKeeperMiniMapIcon != null)
        {
            zooKeeperMiniMapIcon.SetActive(false);
        }
    }

    private void InitializeObjectivesForLevel()
    {
        objectives.Clear(); // Clear any pre-existing objectives

        objectives.Add(new Objective("Eliminate 5 Animals", 5)); // First objective is common across levels

        // Level-specific second objective
        switch (level)
        {
            case 3:
                objectives.Add(new Objective("Confront the Zoo Keeper", 1));
                break;
            default: // Levels 1 and 2
                objectives.Add(new Objective("Get in the truck", 1));
                break;
        }
    }

    private void Update()
    {
        // Update mini-map icons visibility and position based on objectives
        if (objectives[currentObjectiveIndex].description == "Get in the truck" && !objectives[currentObjectiveIndex].isCompleted)
        {
            UpdateTruckIconPosition();
        }
        else if (objectives[currentObjectiveIndex].description == "Confront the Zoo Keeper" && !objectives[currentObjectiveIndex].isCompleted)
        {
            UpdateZooKeeperIconPosition();
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
        if (objectives.Count == 0 || currentObjectiveIndex >= objectives.Count)
        {
            Debug.LogError("Objective list is empty or index is out of bounds.");
            objectiveText.text = "No objectives available.";
            return;
        }

        var currentObjective = objectives[currentObjectiveIndex];
        objectiveText.text = $"{currentObjective.description}: {currentObjective.currentCount}/{currentObjective.targetCount} - {(currentObjective.isCompleted ? "Completed" : "In Progress")}";

        // Show mini-map icons based on the current objective
        if (currentObjective.description == "Get in the truck" && !currentObjective.isCompleted)
        {
            truckMiniMapIcon.SetActive(true);
            zooKeeperMiniMapIcon.SetActive(false);
        }
        else if (currentObjective.description == "Confront the Zoo Keeper" && !currentObjective.isCompleted)
        {
            zooKeeperMiniMapIcon.SetActive(true);
            truckMiniMapIcon.SetActive(false);
        }
        else
        {
            truckMiniMapIcon.SetActive(false);
            zooKeeperMiniMapIcon.SetActive(false);
        }
    }

    private void UpdateTruckIconPosition()
    {
        if (truckMiniMapIcon == null || truckTransform == null || playerTransform == null) return;

        UpdateIconPosition(truckMiniMapIcon, truckTransform);
    }

    private void UpdateZooKeeperIconPosition()
    {
        if (zooKeeperMiniMapIcon == null || zooKeeperTransform == null || playerTransform == null) return;

        UpdateIconPosition(zooKeeperMiniMapIcon, zooKeeperTransform);
    }

    // Generalized method to update mini-map icon positions
    private void UpdateIconPosition(GameObject icon, Transform targetTransform)
    {
        Vector3 targetPosition = targetTransform.position;
        Vector3 playerPosition = playerTransform.position;

        // Calculate the relative position of the target from the player
        Vector3 relativePosition = targetPosition - playerPosition;

        // Keep the icon within the mini-map radius
        if (relativePosition.magnitude > miniMapRadius)
        {
            // Move icon to the edge of the mini-map in the direction of the target
            relativePosition = relativePosition.normalized * miniMapRadius;
        }

        // Update the icon's position in the UI
        icon.transform.localPosition = new Vector3(relativePosition.x, relativePosition.z, 0);
    }

    public bool IsObjectiveComplete(string description)
    {
        var objective = objectives.Find(o => o.description == description);
        return objective != null && objective.isCompleted;
    }
}
