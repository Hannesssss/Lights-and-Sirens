using UnityEngine;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> nextWaypoints = new List<Waypoint>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.2f);

        Gizmos.color = Color.yellow;
        foreach (var next in nextWaypoints)
        {
            if (next != null)
                Gizmos.DrawLine(transform.position, next.transform.position);
        }
    }

    public Waypoint GetNextWaypoint()
    {
        if (nextWaypoints.Count == 0) return null;
        return nextWaypoints[Random.Range(0, nextWaypoints.Count)];
    }
}
