using UnityEngine;

public class CarFollow : MonoBehaviour
{
    public SplineRoute route;
    public float duration = 10f; // Time to complete the route
    private float progress = 0f;

    public float minSpeed = 2f;
    public float maxSpeed = 5f;
    public float acceleration = 2f;

    private float currentSpeed;

    void Start()
    {
        currentSpeed = maxSpeed;
    }

    void Update()
    {
        if (route == null || route.controlPoints.Count < 4) return;

        // Smoothly adjust speed (we'll hook this to traffic behavior later)
        currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);

        // Move along spline based on distance/time
        float distance = currentSpeed * Time.deltaTime;
        float deltaT = distance / (route.totalTime * currentSpeed); // rough estimation
        progress = Mathf.Clamp01(progress + deltaT);

        Vector3 newPos = route.GetPosition(progress);
        transform.position = newPos;

        // Rotate toward forward direction
        Vector3 future = route.GetPosition(Mathf.Clamp01(progress + 0.01f));
        Vector3 dir = (future - newPos).normalized;
        if (dir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);
        }

        // Loop for now (temporary test behavior)
        if (progress >= 1f)
        {
            progress = 0f;
        }
    }
}
