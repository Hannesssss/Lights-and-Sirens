using System.Collections.Generic;
using UnityEngine;

public class FireEventManager : MonoBehaviour
{
    public float timeBetweenFires = 10f;
    private float timer;

    private List<FireController> fireTargets = new();

    void Start()
    {
        // Automatically find all objects tagged as "FireTarget"
        GameObject[] targets = GameObject.FindGameObjectsWithTag("FireTarget");
        foreach (GameObject go in targets)
        {
            FireController fc = go.GetComponentInChildren<FireController>();
            if (fc != null)
                fireTargets.Add(fc);
        }

        Debug.Log("🔥 Found " + fireTargets.Count + " fire targets.");
        timer = timeBetweenFires;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            TryIgniteRandomFire();
            timer = timeBetweenFires;
        }
    }

    void TryIgniteRandomFire()
    {
        List<FireController> unlitFires = fireTargets.FindAll(f => !f.isBurning);

        if (unlitFires.Count > 0)
        {
            int index = Random.Range(0, unlitFires.Count);
            unlitFires[index].Ignite();
            Debug.Log("🔥 Ignited: " + unlitFires[index].gameObject.name);
        }
        else
        {
            Debug.Log("✅ All fires are currently burning. No fire ignited.");
        }
    }
}
