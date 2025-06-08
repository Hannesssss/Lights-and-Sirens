using UnityEngine;
using UnityEngine.AI;

public class VehicleMover : MonoBehaviour
{
    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 target)
    {
        agent.SetDestination(target);
    }
}
