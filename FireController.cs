using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FireController : MonoBehaviour
{
    public float fireHealth = 100f;
    public bool isBurning = false;
    public Slider fireHealthSlider;

    private Renderer fireRenderer;
    private Material originalMaterial;
    private ParticleSystem[] fireParticles;

    void Awake()
    {
        fireRenderer = GetComponent<Renderer>();
        fireParticles = GetComponentsInChildren<ParticleSystem>(true); // includes inactive

        if (fireRenderer != null)
            originalMaterial = new Material(fireRenderer.material); // store original color safely

        if (fireHealthSlider == null)
            fireHealthSlider = GetComponentInChildren<Slider>(true);

        if (fireHealthSlider != null)
        {
            fireHealthSlider.maxValue = fireHealth;
            fireHealthSlider.value = fireHealth;
            fireHealthSlider.gameObject.SetActive(false);
        }

        // Do NOT set to black — use default appearance
        DisableFireVisuals();
    }

    void Update()
    {
        if (isBurning && fireHealthSlider != null)
        {
            fireHealthSlider.value = fireHealth;
        }
    }

    public void Ignite()
    {
        if (isBurning) return;

        fireHealth = 100f;
        isBurning = true;

        foreach (var ps in fireParticles)
            ps.Play();

        if (fireHealthSlider != null)
        {
            fireHealthSlider.value = fireHealth;
            fireHealthSlider.gameObject.SetActive(true);
        }

        if (fireRenderer != null && originalMaterial != null)
            fireRenderer.material.color = originalMaterial.color;

        Debug.Log("🔥 Fire ignited at: " + gameObject.name);
    }

    public void Extinguish(float amount)
    {
        if (!isBurning) return;

        fireHealth -= amount;

        if (fireHealth <= 0)
        {
            fireHealth = 0;
            isBurning = false;
            Debug.Log("🔥 Fire extinguished at: " + gameObject.name);
            DisableFire();
        }
    }

    private void DisableFire()
    {
        foreach (var ps in fireParticles)
            ps.Stop();

        if (fireHealthSlider != null)
            fireHealthSlider.gameObject.SetActive(false);

        if (fireRenderer != null)
            fireRenderer.material.color = Color.black; // simulate burned

        StartCoroutine(RepairBuildingAfterDelay(5f)); // auto-restore after 5 sec
    }

    private void DisableFireVisuals()
    {
        foreach (var ps in fireParticles)
            ps.Stop();

        if (fireHealthSlider != null)
            fireHealthSlider.gameObject.SetActive(false);
    }

    private IEnumerator RepairBuildingAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (fireRenderer != null && originalMaterial != null)
            fireRenderer.material.color = originalMaterial.color;

        Debug.Log("🏗️ Repaired: " + gameObject.name);
    }
}
