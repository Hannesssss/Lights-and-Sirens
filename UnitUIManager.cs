using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitUIManager : MonoBehaviour
{
    public GameObject unitButtonTemplate;
    public Transform unitPanel;

    private readonly List<GameObject> activeButtons = new();

    public void RefreshUnitUI(List<FireTruckController> selectedUnits)
    {
        // Hide the panel if nothing is selected
        unitPanel.gameObject.SetActive(selectedUnits.Count > 0);

        // Remove old buttons
        foreach (var btn in activeButtons)
        {
            Destroy(btn);
        }
        activeButtons.Clear();

        // Sort by unit ID
        selectedUnits.Sort((a, b) => string.Compare(a.unitID, b.unitID));

        // Add new buttons
        foreach (FireTruckController truck in selectedUnits)
        {
            GameObject btn = Instantiate(unitButtonTemplate, unitPanel);
            btn.SetActive(true); // make visible

            // Set text
            TextMeshProUGUI text = btn.transform.Find("UnitNumber")?.GetComponent<TextMeshProUGUI>();
            if (text != null)
                text.text = truck.unitID;

            // (Optional) Set icon sprite here if desired later

            // Click = select this truck
            Button button = btn.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() =>
                {
                    FireTruckController.DeselectAll();
                    truck.Select();
                });
            }

            activeButtons.Add(btn);
        }
    }
}
