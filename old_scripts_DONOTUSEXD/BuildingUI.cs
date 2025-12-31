using UnityEngine;

public class BuildingUI : MonoBehaviour
{
    public static BuildingUI Instance;

    public GameObject panel;
    public Transform content;
    public GameObject buttonPrefab;

    Building current;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void Open(Building building)
    {
        current = building;
        panel.SetActive(true);

        foreach (Transform c in content)
            Destroy(c.gameObject);

        if (building.data.producibleUnits == null) return;

        var queue = building.GetComponent<ProductionQueue>();

        foreach (var unit in building.data.producibleUnits)
        {
            var btn = Instantiate(buttonPrefab, content);
            btn.GetComponentInChildren<TMPro.TMP_Text>().text = unit.unitName;
            btn.GetComponent<UnityEngine.UI.Button>().onClick
                .AddListener(() => queue.Enqueue(unit));
        }
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}
