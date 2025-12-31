using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameSetupMenu : MonoBehaviour
{
    public static int playerTeam = 0;

    [Header("UI References")]
    public Button startGameButton;
    public TMP_Text startGameText;

    [Header("Team Buttons")]
    public Button teamNAButton;
    public Button teamEUButton;

    void Start()
    {
        startGameButton.interactable = false;
        startGameText.text = "Choose Spawn Location";
        startGameText.color = Color.white;
    }

    public void SelectTeam(int team)
    {
        playerTeam = team;
        Debug.Log("Team selected: " + team);

        startGameButton.interactable = true;
        startGameText.text = "Start Game";
        startGameText.color = Color.green;

        teamNAButton.GetComponent<Outline>().enabled = false;
        teamEUButton.GetComponent<Outline>().enabled = false;

        if (team == 1) teamNAButton.GetComponent<Outline>().enabled = true;
        if (team == 2) teamEUButton.GetComponent<Outline>().enabled = true;
    }

    public void StartGame()
    {
        if (playerTeam == 0)
        {
            Debug.LogWarning("Nevybral jsi spawn!");
            return;
        }

        SceneManager.LoadScene("GameScene");
    }
}
