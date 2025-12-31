using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class SelectableUnit : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float stopDistance = 0.05f;

    [Header("Separation (anti-clump)")]
    public float separationRadius = 0.6f;   // jak daleko hledá sousedy
    public float separationStrength = 2.5f; // jak silnì se odtlaèí
    public LayerMask unitLayer;             // nastav na "Units" layer

    [Header("Visual")]
    public GameObject selectionCircle;

    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private bool hasTarget;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        if (selectionCircle != null)
            selectionCircle.SetActive(false);

        targetPosition = rb.position;
    }

    void Update()
    {
        Vector2 pos = rb.position;

        // 1) Smìr k cíli
        Vector2 desired = Vector2.zero;
        if (hasTarget)
        {
            Vector2 toTarget = targetPosition - pos;
            float dist = toTarget.magnitude;

            if (dist <= stopDistance)
            {
                hasTarget = false;
            }
            else
            {
                desired = toTarget.normalized * moveSpeed;
            }
        }

        // 2) Separation (odpuzování od ostatních)
        Vector2 sep = ComputeSeparation(pos);

        // 3) Výsledná rychlost
        Vector2 velocity = desired + sep;

        // omezíme rychlost (a to nekopne do extrému)
        if (velocity.magnitude > moveSpeed)
            velocity = velocity.normalized * moveSpeed;

        // 4) MovePosition
        rb.MovePosition(pos + velocity * Time.deltaTime);
    }

    Vector2 ComputeSeparation(Vector2 pos)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(pos, separationRadius, unitLayer);

        if (hits == null || hits.Length == 0)
            return Vector2.zero;

        Vector2 force = Vector2.zero;
        int count = 0;

        foreach (var h in hits)
        {
            if (h == null) continue;
            if (h.attachedRigidbody == rb) continue;

            Vector2 otherPos = h.bounds.center;
            Vector2 away = pos - otherPos;
            float dist = away.magnitude;

            if (dist < 0.001f) continue;

            // èím blíž, tím silnìjší
            float strength = (1f - (dist / separationRadius)) * separationStrength;
            force += away.normalized * strength;
            count++;
        }

        if (count == 0) return Vector2.zero;

        return force / count;
    }

    public void MoveTo(Vector3 worldTarget)
    {
        targetPosition = new Vector2(worldTarget.x, worldTarget.y);
        hasTarget = true;
    }

    public void SetSelected(bool selected)
    {
        if (selectionCircle != null)
            selectionCircle.SetActive(selected);
    }

    // Jen pro ladìní v editoru (uvidíš radius)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, separationRadius);
    }
}
