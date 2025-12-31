using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [Header("Target Scene (název scény, kterou má tlaèítko otevøít)")]
    public string targetScene;

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(targetScene))
        {
            SceneManager.LoadScene(targetScene);
        }
        else
        {
            Debug.LogWarning("Tlaèítko nemá nastavený targetScene!");
        }
    }
}

