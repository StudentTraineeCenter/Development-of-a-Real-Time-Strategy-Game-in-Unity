using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Build/Category")]
public class BuildCategory : ScriptableObject
{
    public string categoryName;
    public List<BuildingData> buildings = new List<BuildingData>();
}
