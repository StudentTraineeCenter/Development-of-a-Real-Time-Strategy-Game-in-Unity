using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Building : MonoBehaviour
{
    public BuildingData data;

    private void OnMouseDown()
    {
        if (data == null)
        {
            Debug.LogError($"{name}: Chybí BuildingData v Building komponentì!");
            return;
        }

        if (BuildingUI.Instance == null)
        {
            Debug.LogError("BuildingUI.Instance je NULL. Ve scénì chybí BuildingUI objekt se skriptem BuildingUI.");
            return;
        }

        BuildingUI.Instance.Open(this);
    }
}
