using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CategoryPanel : MonoBehaviour
{
    public TMP_Text titleText;
    public Button toggleButton;      // tlaèítko pro collapse/expand
    public GameObject contentRoot;   // root, který se zapíná/vypíná (obsahuje Grid)
    public Transform contentParent;  // kam se budou Instantiate buttony vkládat

    void Awake()
    {
        if (toggleButton != null)
            toggleButton.onClick.AddListener(Toggle);
    }

    public void Init(BuildCategory cat, GameObject buildingButtonPrefab)
    {
        if (titleText != null) titleText.text = cat.categoryName;
        // vyèisti staré
        foreach (Transform c in contentParent) Destroy(c.gameObject);

        foreach (var b in cat.buildings)
        {
            GameObject btn = Instantiate(buildingButtonPrefab, contentParent);
            var bb = btn.GetComponent<BuildingButton>();
            if (bb != null) bb.Init(b);
        }
    }

    public void Toggle()
    {
        if (contentRoot != null) contentRoot.SetActive(!contentRoot.activeSelf);
    }
}
