using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by Joshua Guerrero
 * This script represents the objective
 * types and will help manage objective states
 */
public class Objective : MonoBehaviour
{
    public string Description { get; private set; }
    public bool IsCompleted { get; private set; }
    public int RequiredCount { get; private set; }
    public int CurrentCount { get; private set; }

    public Objective(string description, int requiredCount = 1)
    {
        Description = description;
        RequiredCount = requiredCount;
        CurrentCount = 0;
        IsCompleted = false;
    }

    public void CompleteObjective()
    {
        CurrentCount++;
        if (CurrentCount >= RequiredCount)
        {
            IsCompleted = true;
        }
    }
}
