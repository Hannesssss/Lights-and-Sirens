using UnityEngine;
using UnityEngine.AI;

public class SimpleCarAI : MonoBehaviour
{
    public Transform target; // Assign in Inspector
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }
}
