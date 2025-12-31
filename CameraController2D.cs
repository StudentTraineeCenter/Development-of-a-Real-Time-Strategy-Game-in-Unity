using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    public float moveSpeed = 20f;   // rychlost pohybu
    public float zoomSpeed = 5f;    // rychlost zoomu
    public float minZoom = 3f;      // minimální pøiblížení
    public float maxZoom = 20f;     // maximální oddálení

    public SpriteRenderer mapRenderer; // sem pøetáhni sprite mapy v Inspectoru

    private Camera cam;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    void Start()
    {
        cam = Camera.main;

        // hranice mapy (zjištìné z velikosti sprite)
        if (mapRenderer != null)
        {
            Bounds bounds = mapRenderer.bounds;
            mapMinX = bounds.min.x;
            mapMaxX = bounds.max.x;
            mapMinY = bounds.min.y;
            mapMaxY = bounds.max.y;
        }
    }

    void Update()
    {
        HandleMovement();
        HandleZoom();
        ClampCamera();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D nebo šipky
        float moveY = Input.GetAxisRaw("Vertical");   // W/S nebo šipky
        Vector3 move = new Vector3(-moveX, moveY, 0).normalized;

        // rychlost pohybu závislá na zoomu (pøi oddálení rychlejší)
        transform.position += move * moveSpeed * Time.deltaTime * (cam.orthographicSize / 5f);
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            cam.orthographicSize -= scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }
    }

    void ClampCamera()
    {
        if (mapRenderer == null) return;

        float camHeight = cam.orthographicSize;
        float camWidth = cam.aspect * camHeight;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
