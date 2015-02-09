using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GoTo : MonoBehaviour {

    public EventSystem es; 
    public GameObject MainMenuPanel;
    public GameObject SelectModePanel;
    public GameObject CreditsPanel;
    public GameObject GameTypePanel;
    public GameObject AchievementButton;

    public GameObject firstButtonStartPage;
    public GameObject firstButtonCreditsPage;
    public GameObject firstButtonSelectModePage;
    public GameObject firstButtonGameTypePage;

    void Start()
    {
        if(MainMenuPanel != null)
        {
            //es.SetSelectedGameObject(firstButtonStartPage);
            MainMenuPanel.SetActive(true);
            AchievementButton.SetActive(true);
            SelectModePanel.SetActive(false);
            CreditsPanel.SetActive(false);
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
        Application.LoadLevel("TutorialGridScene01");
        BattleStats.singlePlayer = false;
    }

    public void LAN()
    {
        Application.LoadLevel("LANLobby");
        BattleStats.singlePlayer = false;
    }

    public void StartScreen()
    {
        //es.SetSelectedGameObject(firstButtonStartPage);
        MainMenuPanel.SetActive(true);
        AchievementButton.SetActive(true);
        SelectModePanel.SetActive(false);
        CreditsPanel.SetActive(false);
        GameTypePanel.SetActive(false);
    }

    public void MainMenu()
    {
        Application.LoadLevel("StartScreen");
        Time.timeScale = 1;
    }

    public void Credits()
    {
        //es.SetSelectedGameObject(firstButtonCreditsPage);
        MainMenuPanel.SetActive(false);
        AchievementButton.SetActive(false);
        SelectModePanel.SetActive(false);
        CreditsPanel.SetActive(true);
        GameTypePanel.SetActive(false);
    }

    public void Settings()
    {
        //es.SetSelectedGameObject(firstButtonOptionsPage);
        MainMenuPanel.SetActive(false);
        AchievementButton.SetActive(false);
        SelectModePanel.SetActive(false);
        CreditsPanel.SetActive(false);
        GameTypePanel.SetActive(false);
    }

    public void MultiplayerMode()
    {
        //es.SetSelectedGameObject(firstButtonSelectModePage);
        MainMenuPanel.SetActive(false);
        AchievementButton.SetActive(false);
        SelectModePanel.SetActive(true);
        CreditsPanel.SetActive(false);
        GameTypePanel.SetActive(false);
    }

    public void GameType()
    {
        //es.SetSelectedGameObject(firstButtonGameTypePage);
        MainMenuPanel.SetActive(false);
        AchievementButton.SetActive(false);
        SelectModePanel.SetActive(false);
        CreditsPanel.SetActive(false);
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
