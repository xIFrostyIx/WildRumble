using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 *
 * 
 * NOTE: Stats can be changed based off enemy type
 */

public class EnemyAI_Bear2 : MonoBehaviour
{
    Animator myAnimator; // Used for animations
    public Transform player; // Assign the player object in the Inspector
    public float chaseRange = 10f; // The range at which the AI will start chasing the player
    public float stoppingDistance = 2f; // The distance at which the AI will stop chasing
    public float wanderRange = 5f; // The range within which the AI will wander
    public float wanderTimer = 2f; // Time interval for wandering
    public float speed = 3.5f; //Enemies speed

    private NavMeshAgent navMeshAgent;
    private float timer;

    IEnumerator Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;// set initial speed
        timer = wanderTimer; // Initialize the timer
        myAnimator = GetComponent<Animator>(); //Grabs animation component/controller

        while (true)
        {
            yield return new WaitForSeconds(3);

            myAnimator.SetInteger("bearIdle_Index2", Random.Range(0, 3));
            myAnimator.SetTrigger("bearIdle2");
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        myAnimator.SetFloat("Speed2", navMeshAgent.velocity.magnitude);

        // Check if the player is within chase range
        if (distanceToPlayer < chaseRange)
        {
            // Move towards the player if outside the stopping distance
            if (distanceToPlayer > stoppingDistance)
            {
                navMeshAgent.SetDestination(player.position);

                myAnimator.SetBool("bearWalk2", true);
                myAnimator.SetBool("bearAttack2", false);
                myAnimator.SetBool("bearIdle_Bool2", false);
            }
            else
            {
                navMeshAgent.SetDestination(transform.position); // Stop moving

                myAnimator.SetBool("bearAttack2", true);
                myAnimator.SetBool("bearWalk2", false);
                myAnimator.SetBool("bearIdle_Bool2", false);
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
