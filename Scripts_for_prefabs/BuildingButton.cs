using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingButton : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text nameText;
    public BuildingData data;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        if (button != null) button.onClick.AddListener(OnClick);
    }

    public void Init(BuildingData d)
    {
        data = d;
        if (nameText != null) nameText.text = d != null ? d.buildingName : "Empty";
        if (iconImage != null) iconImage.sprite = d != null ? d.icon : null;
    }

    void OnClick()
    {
        // TODO: tady spustíš "placing mode" nebo zobrazíš detail
        Debug.Log("Clicked build: " + (data != null ? data.buildingName : "<null>"));
    }
}
