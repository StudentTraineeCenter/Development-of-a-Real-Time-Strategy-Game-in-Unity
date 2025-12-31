using UnityEngine;
using System.Collections.Generic;

public class SelectionManager2D : MonoBehaviour
{
    public Camera cam; // pøetáhni Main Camera v Inspectoru
    private List<Unit2D> selected = new List<Unit2D>();

    // LayerMask pro vojáèky (pokud budou mít vlastní vrstvu, zmìò)
    public LayerMask unitLayerMask = ~0; // ~0 = všechny vrstvy

    void Update()
    {
        HandleSelection();
        HandleMovement();
    }

    void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0)) // levý klik
        {
            // správnì nastavená Z od kamery k herní rovinì
            Vector3 mouseScreen = Input.mousePosition;
            mouseScreen.z = cam.transform.position.z - 1f; // Soldier Z = 1
            Vector2 mouseWorld = cam.ScreenToWorldPoint(mouseScreen);

            // Raycast do 2D
            RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.zero, Mathf.Infinity, unitLayerMask);

            if (hit.collider != null)
            {
                Unit2D unit = hit.collider.GetComponent<Unit2D>();
                if (unit != null)
                {
                    ClearSelection();
                    unit.Select(true);
                    selected.Add(unit);
                }
            }
            else
            {
                ClearSelection();
            }
        }
    }

    void HandleMovement()
    {
        if (Input.GetMouseButtonDown(1) && selected.Count > 0) // pravý klik
        {
            Vector3 mouseScreen = Input.mousePosition;
            mouseScreen.z = cam.transform.position.z - 1f; // zachovat Z=1 pro jednotky
            Vector3 mouseWorld = cam.ScreenToWorldPoint(mouseScreen);
            mouseWorld.z = 1f; // pevnì Z jednotky

            foreach (var u in selected)
            {
                u.SetTarget(mouseWorld);
            }
        }
    }

    void ClearSelection()
    {
        foreach (var u in selected)
        {
            u.Select(false);
        }
        selected.Clear();
    }
}
