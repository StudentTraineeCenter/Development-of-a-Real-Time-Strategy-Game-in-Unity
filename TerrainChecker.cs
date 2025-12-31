using UnityEngine;
using UnityEngine.Tilemaps;

public enum TerrainType { Land, Water, Any }

public class TerrainChecker : MonoBehaviour
{
    public static TerrainChecker Instance;

    public Tilemap water;
    public Tilemap land;

    private void Awake()
    {
        Instance = this;
    }

    public TerrainType GetTerrainAt(Vector3 worldPos)
    {
        Vector3Int cell = land.WorldToCell(worldPos);

        if (land != null && land.HasTile(cell)) return TerrainType.Land;
        if (water != null && water.HasTile(cell)) return TerrainType.Water;
        return TerrainType.Any;
    }
}
