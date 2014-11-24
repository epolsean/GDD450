using UnityEngine;
using System.Collections;

public class GoTo : MonoBehaviour {

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
        Application.LoadLevel("StartScreen");
    }

    public void Credits()
    {
        Application.LoadLevel("CreditsScene");
    }

    public void Settings()
    {
        Application.LoadLevel("SettingsScene");
    }

    public void MultiplayerMode()
    {
        Application.LoadLevel("SelectMode");
    }

    public void Battle()
    {
        Application.LoadLevelAdditive("BattleTest");
        BattleStats.player1Pref = "third";
        Destroy(GameObject.Find("CameraAddative"));
    }
}
