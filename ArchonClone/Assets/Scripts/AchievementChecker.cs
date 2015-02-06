using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementChecker : MonoBehaviour 
{

    public GameObject KillsWithAlienGrunt;
    public GameObject KillsWithAlienTank;
    public GameObject KillsWithAlienRunner;
    public GameObject KillsWithAlienScout;
    public GameObject KillsWithAlienBomber;

    public GameObject KillsWithRobotGrunt;
    public GameObject KillsWithRobotTank;
    public GameObject KillsWithRobotRunner;
    public GameObject KillsWithRobotScout;
    public GameObject KillsWithRobotBomber;

	// Use this for initialization
	void Start () 
    {
        CheckForAchievements();
	}

    public void CheckForAchievements()
    {
        //Check for Alien grunt kills
        if (PlayerPrefs.HasKey("KillsWithAlienGrunt"))
        {
            if (PlayerPrefs.GetInt("KillsWithAlienGrunt") >= 10)
                KillsWithAlienGrunt.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        //Check for Alien tank kills
        if (PlayerPrefs.HasKey("KillsWithAlienTank"))
        {
            if (PlayerPrefs.GetInt("KillsWithAlienTank") >= 10)
                KillsWithAlienTank.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        //Check for Alien runner kills
        if (PlayerPrefs.HasKey("KillsWithAlienRunner"))
        {
            if (PlayerPrefs.GetInt("KillsWithAlienRunner") >= 10)
                KillsWithAlienRunner.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        //Check for Alien scout kills
        if (PlayerPrefs.HasKey("KillsWithAlienScout"))
        {
            if (PlayerPrefs.GetInt("KillsWithAlienScout") >= 10)
                KillsWithAlienScout.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        //Check for Alien bomber kills
        if (PlayerPrefs.HasKey("KillsWithAlienBomber"))
        {
            if (PlayerPrefs.GetInt("KillsWithAlienBomber") >= 10)
                KillsWithAlienBomber.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        //Check for Robot grunt kills
        if (PlayerPrefs.HasKey("KillsWithRobotGrunt"))
        {
            if (PlayerPrefs.GetInt("KillsWithRobotGrunt") >= 10)
                KillsWithRobotGrunt.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        //Check for Robot tank kills
        if (PlayerPrefs.HasKey("KillsWithRobotTank"))
        {
            if (PlayerPrefs.GetInt("KillsWithRobotTank") >= 10)
                KillsWithRobotTank.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        //Check for Robot runner kills
        if (PlayerPrefs.HasKey("KillsWithRobotRunner"))
        {
            if (PlayerPrefs.GetInt("KillsWithRobotRunner") >= 10)
                KillsWithRobotRunner.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        //Check for Robot scout kills
        if (PlayerPrefs.HasKey("KillsWithRobotScout"))
        {
            if (PlayerPrefs.GetInt("KillsWithRobotScout") >= 10)
                KillsWithRobotScout.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        //Check for Robot bomber kills
        if (PlayerPrefs.HasKey("KillsWithRobotBomber"))
        {
            if (PlayerPrefs.GetInt("KillsWithRobotBomber") >= 10)
                KillsWithRobotBomber.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        if (PlayerPrefs.HasKey("TutorialComplete"))
        {
            
        }
    }
}
