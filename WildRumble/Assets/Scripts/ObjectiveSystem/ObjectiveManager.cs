using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

/*
 * Created by Joshua Guerrero
 * This script handles the lsit of
 * objectives and their completion logic
 */

public class ObjectiveManager : MonoBehaviour
{
    private List<Objective> objectives;

    private void Start()
    {
        objectives = new List<Objective>
        {
            new Objective("Kill 5 Deer", 5),
            new Objective("Walk to the cabin"),
            new Objective("Kill 10 wild animals", 10),
            new Objective("Get in the truck")
        };
    }

    private void Update()
    {
        CheckObjectives();
    }

    public void CheckObjectives()
    {
        foreach (var objective in objectives)
        {
            if (objective.IsCompleted)
                continue;

            //Obective 1: Kill 5 Deer
            /*void OnTriggerEnter(Collider other)
            {
                if (other.CompareTag("Enemy"))
                {
                    KillAnimal(other.gameObject);
                    FindObjectOfType<ObjectiveManager>().CompleteObjective(0); //Update Kill Count
                }
            }
            */
            //Objective 2: Walk To the Cabin

            //Objective 3: Kill Wild Animals

            //Objective 4: Get in the truck
        }
    }

    public void CompleteObjective(int index)
    {
        if (index < 0 || index >= objectives.Count)
            return;

        objectives[index].CompleteObjective();
        Debug.Log($"Objective Completed: {objectives[index].Description}");
    }
}
