using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildMenuManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject categoryPrefab;
    public GameObject buildingButtonPrefab;
    public Transform contentParent; // kam se spawnou kategorie

    [Header("Data")]
    public List<BuildCategory> categories;

    private bool isOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        isOpen = !isOpen;
        gameObject.SetActive(isOpen);
    }

    void Start()
    {
        GenerateMenu();
        gameObject.SetActive(false);
    }

    void GenerateMenu()
    {
        foreach (var cat in categories)
        {
            GameObject catGO = Instantiate(categoryPrefab, contentParent);
            catGO.name = cat.categoryName;

            //nadpis
            catGO.GetComponentInChildren<TMP_Text>().text = cat.categoryName;

            //grid
            Transform grid = catGO.transform.Find("Content");

            foreach (var b in cat.buildings)
            {
                GameObject btn = Instantiate(buildingButtonPrefab, grid);
                btn.name = b.buildingName;

                //ikona & text
                btn.GetComponentInChildren<TMP_Text>().text = b.buildingName;
                btn.GetComponent<Image>().sprite = b.icon;
            }
        }
    }
}
