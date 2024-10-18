using UnityEngine;
using UnityEngine.AI;
public class FleeingEnemy : MonoBehaviour
{
    public Transform player; 
    public float fleeDistance = 5f; 
    public float hearingDistance = 10f; 
    public float speed = 5f; 
    private NavMeshAgent navMeshAgent;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); 
    }
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);  
        if (distanceToPlayer < fleeDistance)
        {
            Flee(); 
        }
    }
    public void HearGunshot()
    {      
        if (Vector3.Distance(transform.position, player.position) < hearingDistance)
        {
            Flee();
        }
    }
    void Flee()
    {
        Vector3 fleeDirection = (transform.position - player.position).normalized; 
        Vector3 newPosition = transform.position + fleeDirection * fleeDistance; 
        navMeshAgent.SetDestination(newPosition);
    }
}
