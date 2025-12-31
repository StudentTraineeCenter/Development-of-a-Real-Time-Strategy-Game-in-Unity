using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    [Header("Placement")]
    public Tilemap groundTilemap;   // pøiøaï Tilemap_Ground
    public LayerMask blockingMask;  // Buildings + Units (collidery), aby nešlo stavìt pøes nì
    public bool snapToTilemap = true;

    public bool IsBuilding => selectedBuilding != null;

    private BuildingData selectedBuilding;
    private GameObject ghostInstance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!IsBuilding) return;

        UpdateGhostPosition();

        if (Input.GetMouseButtonDown(0))
        {
            TryPlace();
        }

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.R))
        {
            CancelBuild();
        }
    }

    public void BeginBuild(BuildingData data)
    {
        selectedBuilding = data;

        if (ghostInstance != null)
            Destroy(ghostInstance);

        ghostInstance = Instantiate(selectedBuilding.prefab);
        SetGhostMode(ghostInstance, true);
    }

    void UpdateGhostPosition()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = -Camera.main.transform.position.z;
        Vector3 world = Camera.main.ScreenToWorldPoint(mouse);
        world.z = 0f;

        if (snapToTilemap && groundTilemap != null)
        {
            Vector3Int cell = groundTilemap.WorldToCell(world);
            world = groundTilemap.GetCellCenterWorld(cell);
            world.z = 0f;
        }

        ghostInstance.transform.position = world;

        bool can = CanPlaceAt(world);
        SetGhostColor(can ? Color.green : Color.red);
    }

    void TryPlace()
    {
        Vector3 pos = ghostInstance.transform.position;
        if (!CanPlaceAt(pos)) return;

        Instantiate(selectedBuilding.prefab, pos, Quaternion.identity);
        CancelBuild();
    }

    bool CanPlaceAt(Vector3 pos)
    {
        // 1) jen na zemi
        if (TerrainChecker.Instance != null)
        {
            var t = TerrainChecker.Instance.GetTerrainAt(pos);
            if (t != TerrainType.Land) return false;
        }

        // 2) nesmí kolidovat s budovami/jednotkami
        // jednoduchá verze: overlap circle
        float radius = 0.35f;
        Collider2D hit = Physics2D.OverlapCircle(pos, radius, blockingMask);
        if (hit != null) return false;

        return true;
    }

    public void CancelBuild()
    {
        selectedBuilding = null;

        if (ghostInstance != null)
            Destroy(ghostInstance);

        ghostInstance = null;
    }

    void SetGhostMode(GameObject obj, bool ghost)
    {
        // vypni collidery u ghostu, aby neblokoval sám sebe
        foreach (var col in obj.GetComponentsInChildren<Collider2D>())
            col.enabled = false;
    }

    void SetGhostColor(Color c)
    {
        if (ghostInstance == null) return;

        foreach (var r in ghostInstance.GetComponentsInChildren<SpriteRenderer>())
            r.color = new Color(c.r, c.g, c.b, 0.55f);
    }
}
