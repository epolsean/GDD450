using UnityEngine;
using System.Collections;

public class GoTo : MonoBehaviour {

    public void Single()
    {
        //Application.LoadLevel("TestingHexTiles");
    }

	public void Local() 
    {
        Application.LoadLevel("TestingHexTiles");
	}

    public void Tutorial()
    {
        Application.LoadLevel("TutorialTestGrid");
    }

    public void LAN()
    {
        Application.LoadLevel("LANLobby");
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
