using UnityEngine;

public class AutoExtinguish : MonoBehaviour
{
    public float extinguishRange = 5f;
    public float extinguishRate = 10f;

    void Update()
    {
        // Look for all active fires in the scene without sorting
        FireController[] allFires = Object.FindObjectsByType<FireController>(FindObjectsSortMode.None);

        foreach (FireController fire in allFires)
        {
            float distance = Vector3.Distance(transform.position, fire.transform.position);

            if (distance < extinguishRange && fire.isBurning)
            {
                fire.Extinguish(extinguishRate * Time.deltaTime);
            }
        }
    }
}
