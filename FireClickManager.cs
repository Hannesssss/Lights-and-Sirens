using System.Collections.Generic;
using UnityEngine;

public class FireClickManager : MonoBehaviour
{
    public UnitUIManager uiManager;

    private List<FireTruckController> selectedUnits = new();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                FireTruckController truck = hit.collider.GetComponent<FireTruckController>();
                if (truck != null)
                {
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    {
                        if (!selectedUnits.Contains(truck))
                        {
                            selectedUnits.Add(truck);
                            truck.Select();
                        }
                    }
                    else
                    {
                        DeselectAll();
                        selectedUnits.Add(truck);
                        truck.Select();
                    }
                }
                else
                {
                    DeselectAll();
                }
            }
            else
            {
                DeselectAll();
            }

            uiManager.RefreshUnitUI(selectedUnits); // ✅ Updated usage
        }

        if (Input.GetMouseButtonDown(1) && selectedUnits.Count > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                foreach (var truck in selectedUnits)
                {
                    truck.MoveTo(hit.point);
                }
            }
        }
    }

    private void DeselectAll()
    {
        foreach (var truck in selectedUnits)
        {
            truck.Deselect();
        }
        selectedUnits.Clear();
    }
}
