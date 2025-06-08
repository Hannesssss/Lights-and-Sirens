using System.Collections.Generic;
using UnityEngine;

public class FireClickManager : MonoBehaviour
{
    public UnitUIManager uiManager;

    public RectTransform selectionBox;
    private bool isDragging;
    private Vector2 dragStart, dragEnd;
    private FireTruckController[] allTrucks;
    private const float dragThreshold = 5f;

    private List<FireTruckController> selectedUnits = new();

    void Start()
    {
        allTrucks = FindObjectsOfType<FireTruckController>();
        if (selectionBox != null)
            selectionBox.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStart = Input.mousePosition;
            if (selectionBox != null)
                selectionBox.gameObject.SetActive(true);
            isDragging = true;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            dragEnd = Input.mousePosition;
            Vector2 size = dragEnd - dragStart;
            selectionBox.pivot = new Vector2(size.x >= 0 ? 0 : 1, size.y >= 0 ? 0 : 1);
            selectionBox.anchoredPosition = dragStart;
            selectionBox.sizeDelta = new Vector2(Mathf.Abs(size.x), Mathf.Abs(size.y));
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            if (selectionBox != null)
                selectionBox.gameObject.SetActive(false);
            isDragging = false;
            dragEnd = Input.mousePosition;

            if ((dragEnd - dragStart).magnitude < dragThreshold)
            {
                HandleClickSelection();
            }
            else
            {
                HandleDragSelection();
            }

            uiManager.RefreshUnitUI(selectedUnits);
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

    private void HandleClickSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            FireTruckController truck = hit.collider.GetComponent<FireTruckController>();
            if (truck != null)
            {
                if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                {
                    DeselectAll();
                }

                if (!selectedUnits.Contains(truck))
                {
                    selectedUnits.Add(truck);
                    truck.Select();
                }
                else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    selectedUnits.Remove(truck);
                    truck.Deselect();
                }
            }
            else if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            {
                DeselectAll();
            }
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            DeselectAll();
        }
    }

    private void HandleDragSelection()
    {
        Rect rect = new Rect(
            Mathf.Min(dragStart.x, dragEnd.x),
            Mathf.Min(dragStart.y, dragEnd.y),
            Mathf.Abs(dragStart.x - dragEnd.x),
            Mathf.Abs(dragStart.y - dragEnd.y)
        );

        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            DeselectAll();
        }

        foreach (var truck in allTrucks)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(truck.transform.position);
            if (rect.Contains(screenPos))
            {
                if (!selectedUnits.Contains(truck))
                {
                    selectedUnits.Add(truck);
                    truck.Select();
                }
            }
        }
    }
}
