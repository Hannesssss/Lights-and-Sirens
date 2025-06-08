// FirefighterController.cs
using UnityEngine;

public class FirefighterController : MonoBehaviour
{
    public bool isSelected;
    private GameObject selectionRing;

    void Awake()
    {
        selectionRing = transform.Find("SelectionRing")?.gameObject;
        if (selectionRing != null)
            selectionRing.SetActive(false);
    }

    public void Select()
    {
        isSelected = true;
        if (selectionRing != null)
            selectionRing.SetActive(true);
    }

    public void Deselect()
    {
        isSelected = false;
        if (selectionRing != null)
            selectionRing.SetActive(false);
    }

    public void MoveTo(Vector3 target)
    {
        // Later we replace this with NavMeshAgent
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 3f);
    }
}
