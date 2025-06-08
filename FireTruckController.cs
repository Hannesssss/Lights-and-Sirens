using UnityEngine;
using System.Collections.Generic;

public class FireTruckController : MonoBehaviour
{
    public static FireTruckController currentlySelectedTruck;
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

    private void OnMouseDown()
    {
        Select();
    }

    public void Select()
    {
        if (currentlySelectedTruck != null && currentlySelectedTruck != this)
        {
            currentlySelectedTruck.Deselect();
        }

        currentlySelectedTruck = this;
        isSelected = true;

        if (selectionRing != null)
            selectionRing.SetActive(true);

        Debug.Log("✅ Selected (UI or click): " + gameObject.name);
    }

    public static void DeselectAll()
    {
        if (currentlySelectedTruck != null)
        {
            currentlySelectedTruck.Deselect();
            currentlySelectedTruck = null;
        }
    }

    public static void CallDismountOnSelectedTruck()
    {
        if (currentlySelectedTruck != null)
        {
            currentlySelectedTruck.DismountCrew();
        }
    }

    public void Deselect()
    {
        isSelected = false;

        if (selectionRing != null)
            selectionRing.SetActive(false);

        if (currentlySelectedTruck == this)
            currentlySelectedTruck = null;

        Debug.Log("❌ Deselected: " + gameObject.name);
    }

    public void OnSelectedExternally()
    {
        currentlySelectedTruck = this;
        isSelected = true;

        if (selectionRing != null)
            selectionRing.SetActive(true);

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
            FirefighterController controller = obj.GetComponent<FirefighterController>();
            if (controller != null)
                crew.Add(controller);
        }
    }

    public void MoveTo(Vector3 destination)
    {
        mover.MoveTo(destination);
    }
}
