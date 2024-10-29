using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : MonoBehaviour
{
    public ObjectiveManager objectiveManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming player has this tag
        {
            // Code to eliminate the deer
            Destroy(gameObject); // or any other elimination logic

            objectiveManager.UpdateObjective("Eliminate 5 deer");
        }
    }
}
