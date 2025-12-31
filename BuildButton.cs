using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    public BuildingData buildingData;
    public Button button;

    void Reset()
    {
        button = GetComponent<Button>();
    }

    void OnEnable()
    {
        if (button == null) button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveListener(HandleClick);
            button.onClick.AddListener(HandleClick);
        }
    }

    void OnDisable()
    {
        if (button != null)
            button.onClick.RemoveListener(HandleClick);
    }

    void HandleClick()
    {
        if (buildingData == null)
        {
            Debug.LogError("BuildButton: buildingData není pøiøazené!");
            return;
        }

        if (BuildManager.Instance == null)
        {
            Debug.LogError("BuildButton: BuildManager.Instance je null (nemáš BuildManager ve scénì?)");
            return;
        }

        BuildManager.Instance.BeginBuild(buildingData);
    }
}
