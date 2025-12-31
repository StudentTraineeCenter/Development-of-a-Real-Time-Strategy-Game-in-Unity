using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public static UnitSelection Instance;

    public LayerMask unitLayer;

    public List<Unit> SelectedUnits = new List<Unit>();

    Vector2 startMousePos;
    Vector2 endMousePos;
    bool isDragging;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = false;

            SelectUnits();
        }
    }

    void SelectUnits()
    {
        ClearSelection();

        Vector2 center = (startMousePos + endMousePos) / 2f;
        Vector2 size = new Vector2(
            Mathf.Abs(startMousePos.x - endMousePos.x),
            Mathf.Abs(startMousePos.y - endMousePos.y)
        );

        if (size.magnitude < 0.1f)
        {
            // JEDEN KLIK
            RaycastHit2D hit = Physics2D.Raycast(
                center,
                Vector2.zero,
                0f,
                unitLayer
            );

            if (hit.collider != null)
            {
                Unit unit = hit.collider.GetComponent<Unit>();
                if (unit != null)
                    AddUnit(unit);
            }
        }
        else
        {
            // DRAG VÝBÌR
            Collider2D[] hits = Physics2D.OverlapBoxAll(
                center,
                size,
                0f,
                unitLayer
            );

            foreach (var col in hits)
            {
                Unit unit = col.GetComponent<Unit>();
                if (unit != null)
                    AddUnit(unit);
            }
        }
    }

    void AddUnit(Unit unit)
    {
        if (SelectedUnits.Contains(unit)) return;

        SelectedUnits.Add(unit);
        unit.SetSelected(true);
    }

    void ClearSelection()
    {
        foreach (var u in SelectedUnits)
            u.SetSelected(false);

        SelectedUnits.Clear();
    }

    void OnDrawGizmos()
    {
        if (!isDragging) return;

        Vector2 current = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 center = (startMousePos + current) / 2f;
        Vector2 size = new Vector2(
            Mathf.Abs(startMousePos.x - current.x),
            Mathf.Abs(startMousePos.y - current.y)
        );

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, size);
    }
}
