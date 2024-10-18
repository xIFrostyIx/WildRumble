using System.Collections.Generic;
using UnityEngine;

public class FleeingEnemyManager : MonoBehaviour
{
    public List<FleeingEnemy> fleeingEnemies; 

    void Start()
    {
        
        fleeingEnemies = new List<FleeingEnemy>(FindObjectsOfType<FleeingEnemy>());
    }

   
    public void NotifyEnemiesOfGunshot()
    {
        foreach (FleeingEnemy enemy in fleeingEnemies)
        {
            enemy.HearGunshot();
        }
    }
}
