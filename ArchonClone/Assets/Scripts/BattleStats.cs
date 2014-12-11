using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleStats : MonoBehaviour {

    public static int player1Health=0;
    public static int player2Health=0;

    public static int player1Speed=0;
    public static int player2Speed=0;

    public static bool singlePlayer = false;

    public static string winner = null;
    public static string player1Pref = "top";
    public static string player2Pref = "top";
    public static bool hotSeat = true;

    public static bool GameOver = false;

    public static int AlienGruntWinsVSRobotGrunt;
    public static int AlienGruntWinsVSRobotScout;
    public static int AlienGruntWinsVSRobotTank;
    public static int AlienGruntWinsVSRobotRunner;

    public static int AlienTankWinsVSRobotGrunt;
    public static int AlienTankWinsVSRobotScout;
    public static int AlienTankWinsVSRobotTank;
    public static int AlienTankWinsVSRobotRunner;

    public static int AlienScoutWinsVSRobotGrunt;
    public static int AlienScoutWinsVSRobotScout;
    public static int AlienScoutWinsVSRobotTank;
    public static int AlienScoutWinsVSRobotRunner;

    public static int AlienRunnerWinsVSRobotGrunt;
    public static int AlienRunnerWinsVSRobotScout;
    public static int AlienRunnerWinsVSRobotTank;
    public static int AlienRunnerWinsVSRobotRunner;

    public static int RobotGruntWinsVSAlienGrunt;
    public static int RobotGruntWinsVSAlienScout;
    public static int RobotGruntWinsVSAlienTank;
    public static int RobotGruntWinsVSAlienRunner;

    public static int RobotTankWinsVSAlienGrunt;
    public static int RobotTankWinsVSAlienScout;
    public static int RobotTankWinsVSAlienTank;
    public static int RobotTankWinsVSAlienRunner;

    public static int RobotScoutWinsVSAlienGrunt;
    public static int RobotScoutWinsVSAlienScout;
    public static int RobotScoutWinsVSAlienTank;
    public static int RobotScoutWinsVSAlienRunner;

    public static int RobotRunnerWinsVSAlienGrunt;
    public static int RobotRunnerWinsVSAlienScout;
    public static int RobotRunnerWinsVSAlienTank;
    public static int RobotRunnerWinsVSAlienRunner;

    void Awake()
    {
        AlienGruntWinsVSRobotGrunt = PlayerPrefs.GetInt("AlienGruntWinsVSRobotGrunt");
        AlienGruntWinsVSRobotScout = PlayerPrefs.GetInt("AlienGruntWinsVSRobotScout");
        AlienGruntWinsVSRobotTank = PlayerPrefs.GetInt("AlienGruntWinsVSRobotTank");
        AlienGruntWinsVSRobotRunner = PlayerPrefs.GetInt("AlienGruntWinsVSRobotRunner");

        AlienTankWinsVSRobotGrunt = PlayerPrefs.GetInt("AlienTankWinsVSRobotGrunt");
        AlienTankWinsVSRobotScout = PlayerPrefs.GetInt("AlienTankWinsVSRobotScout");
        AlienTankWinsVSRobotTank = PlayerPrefs.GetInt("AlienTankWinsVSRobotTank");
        AlienTankWinsVSRobotRunner = PlayerPrefs.GetInt("AlienTankWinsVSRobotRunner");

        AlienScoutWinsVSRobotGrunt = PlayerPrefs.GetInt("AlienScoutWinsVSRobotGrunt");
        AlienScoutWinsVSRobotScout = PlayerPrefs.GetInt("AlienScoutWinsVSRobotScout");
        AlienScoutWinsVSRobotTank = PlayerPrefs.GetInt("AlienScoutWinsVSRobotTank");
        AlienScoutWinsVSRobotRunner = PlayerPrefs.GetInt("AlienScoutWinsVSRobotRunner");

        AlienRunnerWinsVSRobotGrunt = PlayerPrefs.GetInt("AlienRunnerWinsVSRobotGrunt");
        AlienRunnerWinsVSRobotScout = PlayerPrefs.GetInt("AlienRunnerWinsVSRobotScout");
        AlienRunnerWinsVSRobotTank = PlayerPrefs.GetInt("AlienRunnerWinsVSRobotTank");
        AlienRunnerWinsVSRobotRunner = PlayerPrefs.GetInt("AlienRunnerWinsVSRobotRunner");

        RobotGruntWinsVSAlienGrunt = PlayerPrefs.GetInt("RobotGruntWinsVSAlienGrunt");
        RobotGruntWinsVSAlienScout = PlayerPrefs.GetInt("RobotGruntWinsVSAlienScout");
        RobotGruntWinsVSAlienTank = PlayerPrefs.GetInt("RobotGruntWinsVSAlienTank");
        RobotGruntWinsVSAlienRunner = PlayerPrefs.GetInt("RobotGruntWinsVSAlienRunner");

        RobotTankWinsVSAlienGrunt = PlayerPrefs.GetInt("RobotTankWinsVSAlienGrunt");
        RobotTankWinsVSAlienScout = PlayerPrefs.GetInt("RobotTankWinsVSAlienScout");
        RobotTankWinsVSAlienTank = PlayerPrefs.GetInt("RobotTankWinsVSAlienTank");
        RobotTankWinsVSAlienRunner = PlayerPrefs.GetInt("RobotTankWinsVSAlienRunner");

        RobotScoutWinsVSAlienGrunt = PlayerPrefs.GetInt("RobotScoutWinsVSAlienGrunt");
        RobotScoutWinsVSAlienScout = PlayerPrefs.GetInt("RobotScoutWinsVSAlienScout");
        RobotScoutWinsVSAlienTank = PlayerPrefs.GetInt("RobotScoutWinsVSAlienTank");
        RobotScoutWinsVSAlienRunner = PlayerPrefs.GetInt("RobotScoutWinsVSAlienRunner");

        RobotRunnerWinsVSAlienGrunt = PlayerPrefs.GetInt("RobotRunnerWinsVSAlienGrunt");
        RobotRunnerWinsVSAlienScout = PlayerPrefs.GetInt("RobotRunnerWinsVSAlienScout");
        RobotRunnerWinsVSAlienTank = PlayerPrefs.GetInt("RobotRunnerWinsVSAlienTank");
        RobotRunnerWinsVSAlienRunner = PlayerPrefs.GetInt("RobotRunnerWinsVSAlienRunner");
    }

	// Update is called once per frame
	public static void UpdateStats () 
    {
        if (GameOver)
        {
            string path = Application.streamingAssetsPath;

            path += "/TotalWins.txt";
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.WriteLine("ROBOTS:");
                sw.WriteLine("Number of wins Robot Runner vs Alien Grunt .... " +RobotRunnerWinsVSAlienGrunt);
                sw.WriteLine("Number of wins Robot Runner vs Alien Runner .... " + RobotRunnerWinsVSAlienRunner);
                sw.WriteLine("Number of wins Robot Runner vs Alien Tank .... " + RobotRunnerWinsVSAlienTank);
                sw.WriteLine("Number of wins Robot Runner vs Alien Scout .... " + RobotRunnerWinsVSAlienScout);
                sw.WriteLine("-------------------");
                sw.WriteLine("Number of wins Robot Grunt vs Alien Grunt .... " + RobotGruntWinsVSAlienGrunt);
                sw.WriteLine("Number of wins Robot Grunt vs Alien Runner .... " + RobotGruntWinsVSAlienRunner);
                sw.WriteLine("Number of wins Robot Grunt vs Alien Tank .... " + RobotGruntWinsVSAlienTank);
                sw.WriteLine("Number of wins Robot Grunt vs Alien Scout .... " + RobotGruntWinsVSAlienScout);
                sw.WriteLine("-------------------");
                sw.WriteLine("Number of wins Robot Scout vs Alien Grunt .... " + RobotScoutWinsVSAlienGrunt);
                sw.WriteLine("Number of wins Robot Scout vs Alien Runner .... " + RobotScoutWinsVSAlienRunner);
                sw.WriteLine("Number of wins Robot Scout vs Alien Tank .... " + RobotScoutWinsVSAlienTank);
                sw.WriteLine("Number of wins Robot Scout vs Alien Scout .... " + RobotScoutWinsVSAlienScout);
                sw.WriteLine("-------------------");
                sw.WriteLine("Number of wins Robot Tank vs Alien Grunt .... " + RobotTankWinsVSAlienGrunt);
                sw.WriteLine("Number of wins Robot Tank vs Alien Runner .... " + RobotTankWinsVSAlienRunner);
                sw.WriteLine("Number of wins Robot Tank vs Alien Tank .... " + RobotTankWinsVSAlienTank);
                sw.WriteLine("Number of wins Robot Tank vs Alien Scout .... " + RobotTankWinsVSAlienScout);
                sw.WriteLine("-------------------");
                sw.WriteLine("ALIENS:");
                sw.WriteLine("Number of wins Alien Runner vs Robot Grunt .... " + AlienRunnerWinsVSRobotGrunt);
                sw.WriteLine("Number of wins Alien Runner vs Robot Runner .... " + AlienRunnerWinsVSRobotRunner);
                sw.WriteLine("Number of wins Alien Runner vs Robot Tank .... " + AlienRunnerWinsVSRobotTank);
                sw.WriteLine("Number of wins Alien Runner vs Robot Scout .... " + AlienRunnerWinsVSRobotScout);
                sw.WriteLine("-------------------");
                sw.WriteLine("Number of wins Alien Grunt vs Robot Grunt .... " + AlienGruntWinsVSRobotGrunt);
                sw.WriteLine("Number of wins Alien Grunt vs Robot Runner .... " + AlienGruntWinsVSRobotRunner);
                sw.WriteLine("Number of wins Alien Grunt vs Robot Tank .... " + AlienGruntWinsVSRobotTank);
                sw.WriteLine("Number of wins Alien Grunt vs Robot Scout .... " + AlienGruntWinsVSRobotScout);
                sw.WriteLine("-------------------");
                sw.WriteLine("Number of wins Alien Scout vs Robot Grunt .... " + AlienScoutWinsVSRobotGrunt);
                sw.WriteLine("Number of wins Alien Scout vs Robot Runner .... " + AlienScoutWinsVSRobotRunner);
                sw.WriteLine("Number of wins Alien Scout vs Robot Tank .... " + AlienScoutWinsVSRobotTank);
                sw.WriteLine("Number of wins Alien Scout vs Robot Scout .... " + AlienScoutWinsVSRobotScout);
                sw.WriteLine("-------------------");
                sw.WriteLine("Number of wins Alien Tank vs Robot Grunt .... " + AlienTankWinsVSRobotGrunt);
                sw.WriteLine("Number of wins Alien Tank vs Robot Runner .... " + AlienTankWinsVSRobotRunner);
                sw.WriteLine("Number of wins Alien Tank vs Robot Tank .... " + AlienTankWinsVSRobotTank);
                sw.WriteLine("Number of wins Alien Tank vs Robot Scout .... " + AlienTankWinsVSRobotScout);
                sw.WriteLine("-------------------");
            }

            PlayerPrefs.SetInt("AlienGruntWinsVSRobotGrunt",AlienGruntWinsVSRobotGrunt);
            PlayerPrefs.SetInt("AlienGruntWinsVSRobotScout",AlienGruntWinsVSRobotScout);
            PlayerPrefs.SetInt("AlienGruntWinsVSRobotTank",AlienGruntWinsVSRobotTank);
            PlayerPrefs.SetInt("AlienGruntWinsVSRobotRunner",AlienGruntWinsVSRobotRunner);

            PlayerPrefs.SetInt("AlienTankWinsVSRobotGrunt",AlienTankWinsVSRobotGrunt);
            PlayerPrefs.SetInt("AlienTankWinsVSRobotScout",AlienTankWinsVSRobotScout);
            PlayerPrefs.SetInt("AlienTankWinsVSRobotTank",AlienTankWinsVSRobotTank);
            PlayerPrefs.SetInt("AlienTankWinsVSRobotRunner",AlienTankWinsVSRobotRunner);

            PlayerPrefs.SetInt("AlienScoutWinsVSRobotGrunt",AlienScoutWinsVSRobotGrunt);
            PlayerPrefs.SetInt("AlienScoutWinsVSRobotScout",AlienScoutWinsVSRobotScout);
            PlayerPrefs.SetInt("AlienScoutWinsVSRobotTank",AlienScoutWinsVSRobotTank);
            PlayerPrefs.SetInt("AlienScoutWinsVSRobotRunner",AlienScoutWinsVSRobotRunner);

            PlayerPrefs.SetInt("AlienRunnerWinsVSRobotGrunt",AlienRunnerWinsVSRobotGrunt);
            PlayerPrefs.SetInt("AlienRunnerWinsVSRobotScout",AlienRunnerWinsVSRobotScout);
            PlayerPrefs.SetInt("AlienRunnerWinsVSRobotTank",AlienRunnerWinsVSRobotTank);
            PlayerPrefs.SetInt("AlienRunnerWinsVSRobotRunner",AlienRunnerWinsVSRobotRunner);

            PlayerPrefs.SetInt("RobotGruntWinsVSAlienGrunt",RobotGruntWinsVSAlienGrunt);
            PlayerPrefs.SetInt("RobotGruntWinsVSAlienScout",RobotGruntWinsVSAlienScout);
            PlayerPrefs.SetInt("RobotGruntWinsVSAlienTank",RobotGruntWinsVSAlienTank);
            PlayerPrefs.SetInt("RobotGruntWinsVSAlienRunner",RobotGruntWinsVSAlienRunner);

            PlayerPrefs.SetInt("RobotTankWinsVSAlienGrunt",RobotTankWinsVSAlienGrunt);
            PlayerPrefs.SetInt("RobotTankWinsVSAlienScout",RobotTankWinsVSAlienScout);
            PlayerPrefs.SetInt("RobotTankWinsVSAlienTank",RobotTankWinsVSAlienTank);
            PlayerPrefs.SetInt("RobotTankWinsVSAlienRunner",RobotTankWinsVSAlienRunner);

            PlayerPrefs.SetInt("RobotScoutWinsVSAlienGrunt",RobotScoutWinsVSAlienGrunt);
            PlayerPrefs.SetInt("RobotScoutWinsVSAlienScout",RobotScoutWinsVSAlienScout);
            PlayerPrefs.SetInt("RobotScoutWinsVSAlienTank",RobotScoutWinsVSAlienTank);
            PlayerPrefs.SetInt("RobotScoutWinsVSAlienRunner",RobotScoutWinsVSAlienRunner);

            PlayerPrefs.SetInt("RobotRunnerWinsVSAlienGrunt",RobotRunnerWinsVSAlienGrunt);
            PlayerPrefs.SetInt("RobotRunnerWinsVSAlienScout",RobotRunnerWinsVSAlienScout);
            PlayerPrefs.SetInt("RobotRunnerWinsVSAlienTank",RobotRunnerWinsVSAlienTank);
            PlayerPrefs.SetInt("RobotRunnerWinsVSAlienRunner",RobotRunnerWinsVSAlienRunner);

            GameOver = false;
        }
        
	
	}
}
