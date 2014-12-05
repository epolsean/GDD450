using UnityEngine;
using System.Collections;

public class GoTo : MonoBehaviour {

    public GameObject MainMenuPanel;
    public GameObject OptionsPanel;
    public GameObject SelectModePanel;
    public GameObject CreditsPanel;

    void Start()
    {
        MainMenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        SelectModePanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }

    public void Single()
    {
        Application.LoadLevel("TestingHexTiles");
        BattleStats.singlePlayer = true;
    }

	public void Local() 
    {
        Application.LoadLevel("LargeTileBoard");
        BattleStats.singlePlayer = false;
	}

    public void Tutorial()
    {
        Application.LoadLevel("TutorialTestGrid");
        BattleStats.singlePlayer = false;
    }

    public void LAN()
    {
        Application.LoadLevel("LANLobby");
        BattleStats.singlePlayer = false;
    }

    public void StartScreen()
    {
        MainMenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        SelectModePanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }

    public void Credits()
    {
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        SelectModePanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }

    public void Settings()
    {
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(true);
        SelectModePanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }

    public void MultiplayerMode()
    {
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        SelectModePanel.SetActive(true);
        CreditsPanel.SetActive(false);
    }

    public void Battle()
    {
        Application.LoadLevelAdditive("BattleTest");
        BattleStats.player1Pref = "third";
        Destroy(GameObject.Find("CameraAddative"));
    }
}
