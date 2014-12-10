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

    public static int AlienGruntWinsVSRobotGrunt = 0;
    public static int AlienGruntWinsVSRobotScout = 0;
    public static int AlienGruntWinsVSRobotTank = 0;
    public static int AlienGruntWinsVSRobotRunner = 0;

    public static int AlienTankWinsVSRobotGrunt = 0;
    public static int AlienTankWinsVSRobotScout = 0;
    public static int AlienTankWinsVSRobotTank = 0;
    public static int AlienTankWinsVSRobotRunner = 0;

    public static int AlienScoutWinsVSRobotGrunt = 0;
    public static int AlienScoutWinsVSRobotScout = 0;
    public static int AlienScoutWinsVSRobotTank = 0;
    public static int AlienScoutWinsVSRobotRunner = 0;

    public static int AlienRunnerWinsVSRobotGrunt = 0;
    public static int AlienRunnerWinsVSRobotScout = 0;
    public static int AlienRunnerWinsVSRobotTank = 0;
    public static int AlienRunnerWinsVSRobotRunner = 0;

    public static int RobotGruntWinsVSAlienGrunt = 0;
    public static int RobotGruntWinsVSAlienScout = 0;
    public static int RobotGruntWinsVSAlienTank = 0;
    public static int RobotGruntWinsVSAlienRunner = 0;

    public static int RobotTankWinsVSAlienGrunt = 0;
    public static int RobotTankWinsVSAlienScout = 0;
    public static int RobotTankWinsVSAlienTank = 0;
    public static int RobotTankWinsVSAlienRunner = 0;

    public static int RobotScoutWinsVSAlienGrunt = 0;
    public static int RobotScoutWinsVSAlienScout = 0;
    public static int RobotScoutWinsVSAlienTank = 0;
    public static int RobotScoutWinsVSAlienRunner = 0;

    public static int RobotRunnerWinsVSAlienGrunt = 0;
    public static int RobotRunnerWinsVSAlienScout = 0;
    public static int RobotRunnerWinsVSAlienTank = 0;
    public static int RobotRunnerWinsVSAlienRunner = 0;

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
            GameOver = false;
        }
        
	
	}
}
