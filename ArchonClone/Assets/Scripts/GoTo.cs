using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GoTo : MonoBehaviour {

    public EventSystem es; 
    public GameObject MainMenuPanel;
    public GameObject OptionsPanel;
    public GameObject SelectModePanel;
    public GameObject CreditsPanel;
    public GameObject ControlsPanel;
    public GameObject BackToOptionsButton;
    public GameObject SettingsPanel;
    public GameObject GameTypePanel;

    public GameObject firstButtonStartPage;
    public GameObject firstButtonCreditsPage;
    public GameObject firstButtonOptionsPage;
    public GameObject firstButtonSelectModePage;
    public GameObject firstButtonGameTypePage;

    void Start()
    {
        if(MainMenuPanel != null)
        {
            es.SetSelectedGameObject(firstButtonStartPage);
            MainMenuPanel.SetActive(true);
            OptionsPanel.SetActive(false);
            SelectModePanel.SetActive(false);
            CreditsPanel.SetActive(false);
            ControlsPanel.SetActive(false);
            BackToOptionsButton.SetActive(false);
            SettingsPanel.SetActive(false);
            GameTypePanel.SetActive(false);
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
        /*float sceneSelect = Random.Range(0f, 1000f);
        if (sceneSelect < 500)
        {
            Application.LoadLevel("TileBoardScene_01");
        }
        else
        {
            Application.LoadLevel("TestingTileMovement_Ryan");
        }*/
        Application.LoadLevel("TestingTileMovement_Ryan");
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
        es.SetSelectedGameObject(firstButtonStartPage);
        MainMenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        SelectModePanel.SetActive(false);
        CreditsPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        BackToOptionsButton.SetActive(false);
        SettingsPanel.SetActive(false);
        GameTypePanel.SetActive(false);
    }

    public void MainMenu()
    {
        Application.LoadLevel("StartScreen");
    }

    public void Credits()
    {
        es.SetSelectedGameObject(firstButtonCreditsPage);
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        SelectModePanel.SetActive(false);
        CreditsPanel.SetActive(true);
        ControlsPanel.SetActive(false);
        BackToOptionsButton.SetActive(false);
        SettingsPanel.SetActive(false);
        GameTypePanel.SetActive(false);
    }

    public void Settings()
    {
        es.SetSelectedGameObject(firstButtonOptionsPage);
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(true);
        SelectModePanel.SetActive(false);
        CreditsPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        BackToOptionsButton.SetActive(false);
        SettingsPanel.SetActive(false);
        GameTypePanel.SetActive(false);
    }

    public void MultiplayerMode()
    {
        es.SetSelectedGameObject(firstButtonSelectModePage);
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        SelectModePanel.SetActive(true);
        CreditsPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        BackToOptionsButton.SetActive(false);
        SettingsPanel.SetActive(false);
        GameTypePanel.SetActive(false);
    }

    public void GameType()
    {
        es.SetSelectedGameObject(firstButtonGameTypePage);
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        SelectModePanel.SetActive(false);
        CreditsPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        BackToOptionsButton.SetActive(false);
        SettingsPanel.SetActive(false);
        GameTypePanel.SetActive(true);
    }

    public void Battle()
    {
        Application.LoadLevelAdditive("BattleTest");
        BattleStats.player1Pref = "third";
        Destroy(GameObject.Find("CameraAddative"));
    }

    public void Acheivements()
    {
        Application.LoadLevel("AchievementsPage");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
