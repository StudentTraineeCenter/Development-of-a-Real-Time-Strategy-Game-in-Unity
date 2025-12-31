using UnityEngine;

public enum BuildingType
{
    PowerPlant,
    Barracks,
    Factory
}

[CreateAssetMenu(menuName = "Game/Building Data")]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public BuildingType type;
    public GameObject prefab;
    public Sprite icon;
    public int cost;

    [Header("Placement")]
    public Vector2Int footprint = new Vector2Int(2, 2); // kolik tile bunìk zabírá
    public float blockPadding = 0.0f; // extra mezera v world units

    [Header("Production (optional)")]
    public UnitData[] producibleUnits;
}
