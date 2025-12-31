using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool IsSelected { get; private set; }
    public GameObject selectionCircle;

    void Awake()
    {
        if (selectionCircle != null)
            selectionCircle.SetActive(false);
    }

    public void SetSelected(bool selected)
    {
        IsSelected = selected;

        if (selectionCircle != null)
            selectionCircle.SetActive(selected);
    }
    void OnMouseDown()
    {
        Debug.Log("Klik na jednotku funguje: " + name);
    }

}
