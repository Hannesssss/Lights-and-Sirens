using System.Collections.Generic;
using UnityEngine;

public class SplineRoute : MonoBehaviour
{
    public List<Transform> controlPoints = new List<Transform>();

    private SplinePath spline;
    public float totalTime = 5f; // How long a car takes to complete the path

    void Awake()
    {
        spline = new SplinePath(controlPoints);
    }

    public Vector3 GetPosition(float t)
    {
        return spline.GetPosition(t);
    }
}
