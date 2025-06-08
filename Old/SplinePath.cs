using System.Collections.Generic;
using UnityEngine;

public class SplinePath
{
    private List<Transform> points;

    public SplinePath(List<Transform> waypoints)
    {
        this.points = waypoints;
    }

    // Clamp index to be within range and wrap edges if needed
    private Transform GetPoint(int index)
    {
        index = Mathf.Clamp(index, 0, points.Count - 1);
        return points[index];
    }

    // Get a point on the Catmull-Rom spline
    public Vector3 GetPosition(float t)
    {
        if (points.Count < 4)
            return Vector3.zero;

        // Figure out which segment we're in
        int numSections = points.Count - 3;
        int currIndex = Mathf.Min(Mathf.FloorToInt(t * numSections), numSections - 1);

        float segmentT = (t * numSections) - currIndex;

        Vector3 a = GetPoint(currIndex).position;
        Vector3 b = GetPoint(currIndex + 1).position;
        Vector3 c = GetPoint(currIndex + 2).position;
        Vector3 d = GetPoint(currIndex + 3).position;

        return CatmullRom(a, b, c, d, segmentT);
    }

    // Core Catmull-Rom function
    private Vector3 CatmullRom(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        return 0.5f * (
            (2f * b) +
            (-a + c) * t +
            (2f * a - 5f * b + 4f * c - d) * t * t +
            (-a + 3f * b - 3f * c + d) * t * t * t
        );
    }
}
