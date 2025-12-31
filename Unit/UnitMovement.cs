using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public float speed = 3f;

    private Vector3 target;
    private bool moving = false;

    void Update()
    {
        if (!moving) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.05f)
            moving = false;
    }

    public void MoveTo(Vector3 worldPos)
    {
        target = new Vector3(worldPos.x, worldPos.y, transform.position.z);
        moving = true;
    }
}
