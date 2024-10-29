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
    public string description;
    public bool isCompleted;
    public int targetCount;
    public int currentCount;

    public Objective(string description, int targetCount)
    {
        this.description = description;
        this.targetCount = targetCount;
        this.currentCount = 0;
        this.isCompleted = false;
    }

    public void UpdateObjective()
    {
        if (currentCount >= targetCount)
        {
            isCompleted = true;
        }
    }
}
