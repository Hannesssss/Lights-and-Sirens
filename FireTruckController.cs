using UnityEngine;
using System.Collections.Generic;

public class FireTruckController : MonoBehaviour
{
    private VehicleMover mover;

    [Header("Crew Settings")]
    public GameObject firefighterPrefab;
    public Transform[] dismountPoints;
    private readonly List<FirefighterController> crew = new();

    [Header("Selection State")]
    public bool isSelected = false;

    [Header("Unit Info")]
    public string unitID = "1-1"; // Set manually for now

    private GameObject selectionRing;

    private void Awake()
    {
        mover = GetComponent<VehicleMover>();
        selectionRing = transform.Find("SelectionRing")?.gameObject;

        if (selectionRing != null)
            selectionRing.SetActive(false);
    }


    public void Select()
    {
        isSelected = true;

        if (selectionRing != null)
            selectionRing.SetActive(true);

        Debug.Log("✅ Selected (UI or click): " + gameObject.name);
    }

    public static void DeselectAll()
    {
        foreach (var truck in FindObjectsOfType<FireTruckController>())
        {
            truck.Deselect();
        }
    }

    public void Deselect()
    {
        isSelected = false;

        if (selectionRing != null)
            selectionRing.SetActive(false);

        Debug.Log("❌ Deselected: " + gameObject.name);
    }

    public void OnSelectedExternally()
    {
        Select();
        Debug.Log("✅ Selected (UI): " + gameObject.name);
    }

    public void DismountCrew()
    {
        if (firefighterPrefab == null || dismountPoints == null)
            return;

        foreach (var point in dismountPoints)
        {
            if (point == null) continue;
            GameObject obj = Instantiate(firefighterPrefab, point.position, point.rotation);
            FirefighterController ff = obj.GetComponent<FirefighterController>();
            if (ff != null)
                crew.Add(ff);
        }
    }

    public void MoveTo(Vector3 destination)
    {
        mover.MoveTo(destination);
    }
}
