using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    [Header("UI")]
    public RectTransform selectionBox;          // UI Image (SelectionBox) v Canvasu

    [Header("Selection")]
    public LayerMask unitLayer;                 // Layer Units
    public float clickDragThreshold = 10f;      // px - kdy už je to drag

    [Header("Formation")]
    public float formationSpacing = 0.8f;       // rozestup jednotek ve formaci (world units)
    public int maxColumns = 6;                  // max sloupcù ve formaci

    private Camera cam;
    private Vector2 startMousePos;
    private bool isDragging;

    private readonly List<SelectableUnit> selectedUnits = new List<SelectableUnit>();

    void Start()
    {
        cam = Camera.main;
        if (selectionBox != null)
            selectionBox.gameObject.SetActive(false);
    }

    void Update()
    {
        HandleSelection();
        HandleMovement();
    }

    // ---------- INPUT HELPERS ----------
    bool IsPointerOverUI()
    {
        // zabrání tomu, aby UI klik bral i selection
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }

    Vector3 GetMouseWorld()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = -cam.transform.position.z;
        Vector3 world = cam.ScreenToWorldPoint(mouse);
        world.z = 0f;
        return world;
    }

    // ---------- SELECTION ----------
    void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUI()) return;

            startMousePos = Input.mousePosition;
            isDragging = false;

            if (selectionBox != null)
            {
                selectionBox.gameObject.SetActive(true);
                selectionBox.sizeDelta = Vector2.zero;
                selectionBox.position = startMousePos;
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (IsPointerOverUI()) return;

            // pokud už myš ujela víc než threshold, bereme to jako drag
            if (!isDragging && Vector2.Distance((Vector2)Input.mousePosition, startMousePos) > clickDragThreshold)
                isDragging = true;

            if (selectionBox != null)
                UpdateSelectionBox(startMousePos, Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectionBox != null)
                selectionBox.gameObject.SetActive(false);

            if (IsPointerOverUI()) return;

            if (isDragging)
            {
                SelectUnitsInBox();
            }
            else
            {
                // jednoduchý klik select
                SelectSingleUnderMouse();
            }
        }
    }

    void UpdateSelectionBox(Vector2 start, Vector2 end)
    {
        Vector2 center = (start + end) / 2f;
        selectionBox.position = center;

        Vector2 size = new Vector2(
            Mathf.Abs(start.x - end.x),
            Mathf.Abs(start.y - end.y)
        );

        selectionBox.sizeDelta = size;
    }

    void ClearSelection()
    {
        foreach (var u in selectedUnits)
            u.SetSelected(false);

        selectedUnits.Clear();
    }

    void SelectSingleUnderMouse()
    {
        // pokud nedržíš Shift, vyèisti selection
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            ClearSelection();

        Vector3 world = GetMouseWorld();
        RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero, 0f, unitLayer);

        if (hit.collider == null) return;

        SelectableUnit unit = hit.collider.GetComponentInParent<SelectableUnit>();
        if (unit == null) return;

        if (!selectedUnits.Contains(unit))
        {
            selectedUnits.Add(unit);
            unit.SetSelected(true);
        }
    }

    void SelectUnitsInBox()
    {
        // pokud nedržíš Shift, vyèisti selection
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            ClearSelection();

        // Pozor: selectionBox.anchoredPosition je v Canvas prostoru.
        // My používáme selectionBox.position (screen), takže min/max bereme ze screen-space.
        Vector2 boxCenter = selectionBox.position;
        Vector2 halfSize = selectionBox.sizeDelta / 2f;

        Vector2 min = boxCenter - halfSize;
        Vector2 max = boxCenter + halfSize;

        // Pro jednoduchost (a protože zatím máš desítky jednotek max),
        // projdeme všechny SelectableUnit ve scénì.
        foreach (SelectableUnit unit in FindObjectsOfType<SelectableUnit>())
        {
            Vector2 screenPos = cam.WorldToScreenPoint(unit.transform.position);

            if (screenPos.x >= min.x && screenPos.x <= max.x &&
                screenPos.y >= min.y && screenPos.y <= max.y)
            {
                if (!selectedUnits.Contains(unit))
                {
                    selectedUnits.Add(unit);
                    unit.SetSelected(true);
                }
            }
        }
    }

    // ---------- MOVEMENT / FORMATION ----------
    void HandleMovement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (IsPointerOverUI()) return;
            if (selectedUnits.Count == 0) return;

            Vector3 target = GetMouseWorld();
            IssueMoveFormation(target);
        }
    }

    void IssueMoveFormation(Vector3 target)
    {
        int count = selectedUnits.Count;

        // Formace jako møížka (grid) kolem cíle.
        int cols = Mathf.Min(maxColumns, Mathf.CeilToInt(Mathf.Sqrt(count)));
        int rows = Mathf.CeilToInt(count / (float)cols);

        // centrování formace kolem targetu
        float totalW = (cols - 1) * formationSpacing;
        float totalH = (rows - 1) * formationSpacing;
        Vector3 topLeft = target + new Vector3(-totalW / 2f, totalH / 2f, 0f);

        for (int i = 0; i < count; i++)
        {
            int r = i / cols;
            int c = i % cols;

            Vector3 offset = new Vector3(c * formationSpacing, -r * formationSpacing, 0f);
            Vector3 dest = topLeft + offset;

            selectedUnits[i].MoveTo(dest);
        }
    }
}
