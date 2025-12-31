using UnityEngine;

public class MouseTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Klik");
        }
    }
}
