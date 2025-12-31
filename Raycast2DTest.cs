using UnityEngine;

public class Raycast2DTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit =
                Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            Debug.Log("Klik na: " + mouseWorldPos);

            if (hit.collider != null)
            {
                Debug.Log("ZASAŽENO: " + hit.collider.name);
            }
            else
            {
                Debug.Log("NIC ZASAŽENO");
            }
        }
    }
}
