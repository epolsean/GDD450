using UnityEngine;
using System.Collections;

public class AchievementTracker : MonoBehaviour {

    public static int EliminationGamesWon=0;
    public static int DominationGamesWon=0;
    public static int ControlPointsGamesWon=0;
    public static int TotalGamesWon=0;

    public static int KillsWithAlienGrunt=0;
    public static int KillsWithAlienTank=0;
    public static int KillsWithAlienRunner=0;
    public static int KillsWithAlienScout=0;
    public static int KillsWithAlienBomber=0;

    public static int KillsWithRobotGrunt=0;
    public static int KillsWithRobotTank=0;
    public static int KillsWithRobotRunner=0;
    public static int KillsWithRobotScout=0;
    public static int KillsWithRobotBomber=0;

    public static int TutorialComplete=0;


	// Use this for initialization
	void Start () 
    {
        SetAchievements();
	}

    public void ClearStats()
    {
            PlayerPrefs.SetInt("KillsWithAlienGrunt", 0);
            PlayerPrefs.SetInt("KillsWithAlienTank", 0);
            PlayerPrefs.SetInt("KillsWithAlienRunner", 0);
            PlayerPrefs.SetInt("KillsWithAlienScout", 0);
            PlayerPrefs.SetInt("KillsWithAlienBomber", 0);
            PlayerPrefs.SetInt("KillsWithRobotGrunt", 0);
            PlayerPrefs.SetInt("KillsWithRobotTank", 0);
            PlayerPrefs.SetInt("KillsWithRobotRunner", 0);
            PlayerPrefs.SetInt("KillsWithRobotScout", 0);
            PlayerPrefs.SetInt("KillsWithRobotBomber", 0);
            PlayerPrefs.SetInt("TutorialComplete", 0);
    }


    void SetAchievements()
    {
        //Check for Alien grunt kills
        if (PlayerPrefs.HasKey("KillsWithAlienGrunt"))
        {
            KillsWithAlienGrunt = PlayerPrefs.GetInt("KillsWithAlienGrunt");
        }
        else
        {
            PlayerPrefs.SetInt("KillsWithAlienGrunt", KillsWithAlienGrunt);
        }

        //Check for Alien tank kills
        if (PlayerPrefs.HasKey("KillsWithAlienTank"))
        {
            KillsWithAlienTank = PlayerPrefs.GetInt("KillsWithAlienTank");
        }
        else
        {
            PlayerPrefs.SetInt("KillsWithAlienTank", KillsWithAlienTank);
        }

        //Check for Alien runner kills
        if (PlayerPrefs.HasKey("KillsWithAlienRunner"))
        {
            KillsWithAlienRunner = PlayerPrefs.GetInt("KillsWithAlienRunner");
        }
        else
        {
            PlayerPrefs.SetInt("KillsWithAlienRunner", KillsWithAlienRunner);
        }

        //Check for Alien scout kills
        if (PlayerPrefs.HasKey("KillsWithAlienScout"))
        {
            KillsWithAlienScout = PlayerPrefs.GetInt("KillsWithAlienScout");
        }
        else
        {
            PlayerPrefs.SetInt("KillsWithAlienScout", KillsWithAlienScout);
        }

        //Check for Alien bomber kills
        if (PlayerPrefs.HasKey("KillsWithAlienBomber"))
        {
            KillsWithAlienBomber = PlayerPrefs.GetInt("KillsWithAlienBomber");
        }
        else
        {
            PlayerPrefs.SetInt("KillsWithAlienBomber", KillsWithAlienBomber);
        }

        //Check for Robot grunt kills
        if (PlayerPrefs.HasKey("KillsWithRobotGrunt"))
        {
            KillsWithRobotGrunt = PlayerPrefs.GetInt("KillsWithRobotGrunt");
        }
        else
        {
            PlayerPrefs.SetInt("KillsWithRobotGrunt", KillsWithRobotGrunt);
        }

        //Check for Robot tank kills
        if (PlayerPrefs.HasKey("KillsWithRobotTank"))
        {
            KillsWithRobotTank = PlayerPrefs.GetInt("KillsWithRobotTank");
        }
        else
        {
            PlayerPrefs.SetInt("KillsWithRobotTank", KillsWithRobotTank);
        }

        //Check for Robot runner kills
        if (PlayerPrefs.HasKey("KillsWithRobotRunner"))
        {
            KillsWithRobotRunner = PlayerPrefs.GetInt("KillsWithRobotRunner");
        }
        else
        {
            PlayerPrefs.SetInt("KillsWithRobotRunner", KillsWithRobotRunner);
        }

        //Check for Robot scout kills
        if (PlayerPrefs.HasKey("KillsWithRobotScout"))
        {
            KillsWithRobotScout = PlayerPrefs.GetInt("KillsWithRobotScout");
        }
        else
        {
            PlayerPrefs.SetInt("KillsWithRobotScout", KillsWithRobotScout);
        }

        //Check for Robot bomber kills
        if (PlayerPrefs.HasKey("KillsWithRobotBomber"))
        {
            KillsWithRobotBomber = PlayerPrefs.GetInt("KillsWithRobotBomber");
        }
        else
        {
            PlayerPrefs.SetInt("KillsWithRobotBomber", KillsWithRobotBomber);
        }

        if (PlayerPrefs.HasKey("TutorialComplete"))
        {
            TutorialComplete = PlayerPrefs.GetInt("TutorialComplete");
        }
        else
        {
            PlayerPrefs.SetInt("TutorialComplete", TutorialComplete);
        }
    }
}
