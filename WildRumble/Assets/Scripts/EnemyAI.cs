using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Created by: Joshua Guerrero
 * This script sets the behavior of
 * AI enemies in the game.
 * 
 * NOTE: Stats can be changed based off enemy type
 */

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Assign the player object in the Inspector
    public float chaseRange = 10f; // The range at which the AI will start chasing the player
    public float stoppingDistance = 2f; // The distance at which the AI will stop chasing
    public float wanderRange = 5f; // The range within which the AI will wander
    public float wanderTimer = 2f; // Time interval for wandering
    public float speed = 3.5f; //Enemies speed

    private NavMeshAgent navMeshAgent;
    private float timer;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;// set initial speed
        timer = wanderTimer; // Initialize the timer
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within chase range
        if (distanceToPlayer < chaseRange)
        {
            // Move towards the player if outside the stopping distance
            if (distanceToPlayer > stoppingDistance)
            {
                navMeshAgent.SetDestination(player.position);
            }
            else
            {
                navMeshAgent.SetDestination(transform.position); // Stop moving
            }
        }
        else
        {
            Wander();
        }
    }

    void Wander()
    {
        // Update the timer
        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            // Set a new random destination within wander range
            Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
            randomDirection += transform.position; // Offset the random direction by the AI's position
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, wanderRange, NavMesh.AllAreas);
            navMeshAgent.SetDestination(hit.position);

            timer = 0; // Reset the timer
        }
    }
}
