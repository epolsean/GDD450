using UnityEngine;
using System.Collections;

public class GoTo : MonoBehaviour {

    public GameObject MainMenuPanel;
    public GameObject OptionsPanel;
    public GameObject SelectModePanel;
    public GameObject CreditsPanel;
    public GameObject ControlsPanel;
    public GameObject BackToOptionsButton;
    public GameObject SettingsPanel;

    void Start()
    {
        if(MainMenuPanel != null)
        {
            MainMenuPanel.SetActive(true);
            OptionsPanel.SetActive(false);
            SelectModePanel.SetActive(false);
            CreditsPanel.SetActive(false);
            ControlsPanel.SetActive(false);
            BackToOptionsButton.SetActive(false);
            SettingsPanel.SetActive(false);
        }
    }

    public void Single()
    {
        float sceneSelect = Random.Range(0f,1000f);
        if (sceneSelect < 250)
        {
            Application.LoadLevel("TestingHexTiles");
        }
        else if (sceneSelect < 500)
        {
            Application.LoadLevel("LargeTileBoard");
        }
        else if (sceneSelect < 750)
        {
            Application.LoadLevel("LargeTileBoard02");
        }
        else
        {
            Application.LoadLevel("MediumHexBoard02");
        }
        BattleStats.singlePlayer = true;
    }

	public void Local() 
    {
        float sceneSelect = Random.Range(0f, 1000f);
        if (sceneSelect < 250)
        {
            Application.LoadLevel("TestingHexTiles");
        }
        else if (sceneSelect < 500)
        {
            Application.LoadLevel("LargeTileBoard");
        }
        else if (sceneSelect < 750)
        {
            Application.LoadLevel("LargeTileBoard02");
        }
        else
        {
            Application.LoadLevel("MediumHexBoard02");
        }
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
        ControlsPanel.SetActive(false);
        BackToOptionsButton.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    public void MainMenu()
    {
        Application.LoadLevel("StartScreen");
    }

    public void Credits()
    {
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        SelectModePanel.SetActive(false);
        CreditsPanel.SetActive(true);
        ControlsPanel.SetActive(false);
        BackToOptionsButton.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    public void Settings()
    {
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(true);
        SelectModePanel.SetActive(false);
        CreditsPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        BackToOptionsButton.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    public void MultiplayerMode()
    {
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        SelectModePanel.SetActive(true);
        CreditsPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        BackToOptionsButton.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    public void Battle()
    {
        Application.LoadLevelAdditive("BattleTest");
        BattleStats.player1Pref = "third";
        Destroy(GameObject.Find("CameraAddative"));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
