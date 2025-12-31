using UnityEngine;

public enum UnitMovementType
{
    Ground,
    Naval,
    Air
}

[CreateAssetMenu(menuName = "Game/Unit Data")]
public class UnitData : ScriptableObject
{
    public string unitName;
    public UnitMovementType movementType;
    public GameObject prefab;
    public Sprite icon;
    public int cost;
}
