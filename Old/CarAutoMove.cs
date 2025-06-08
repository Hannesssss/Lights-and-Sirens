using UnityEngine;
using UnityEngine.AI;

public class CarAutoMove : MonoBehaviour
{
    public Transform destination; // You’ll assign this in the Inspector
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (destination != null)
        {
            agent.SetDestination(destination.position);
        }
    }
}
