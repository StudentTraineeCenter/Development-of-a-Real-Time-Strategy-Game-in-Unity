using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Unit2D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float stoppingDistance = 0.05f;

    [Header("Selection")]
    [SerializeField] private GameObject selectionIndicator;

    // Runtime state
    private Vector2 targetPos;
    private bool hasTarget = false;
    private bool isSelected = false;

    private SpriteRenderer sr;

    public UnityEvent<Unit2D> OnSelected;
    public UnityEvent<Unit2D> OnDeselected;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        targetPos = transform.position;
        if (selectionIndicator) selectionIndicator.SetActive(false);
    }

    void Update()
    {
        // Barva podle výbìru
        sr.color = isSelected ? Color.green : Color.white;

        // Pohyb smìrem k cíli
        if (hasTarget)
        {
            Vector2 pos2D = Vector2.MoveTowards((Vector2)transform.position, targetPos, speed * Time.deltaTime);
            transform.position = new Vector3(pos2D.x, pos2D.y, transform.position.z);

            if (Vector2.Distance(pos2D, targetPos) <= stoppingDistance)
                hasTarget = false;
        }
    }

    // Nastaví cílovou pozici
    public void SetTarget(Vector3 worldPos)
    {
        targetPos = new Vector2(worldPos.x, worldPos.y);
        hasTarget = true;
    }

    public void Select(bool select)
    {
        if (isSelected == select) return;

        isSelected = select;
        if (selectionIndicator) selectionIndicator.SetActive(select);

        if (select) OnSelected?.Invoke(this);
        else OnDeselected?.Invoke(this);
    }
}
