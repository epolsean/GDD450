using UnityEngine;
using System.Collections;

public class BattleStats : MonoBehaviour {

    public static int player1Health=0;
    public static int player2Health=0;

    public static int player1Speed=0;
    public static int player2Speed=0;

    public static bool singlePlayer = false;

    public static string winner = null;
    public static string player1Pref = "third";
    public static string player2Pref = "top";
    public static bool hotSeat = true;

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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	
	}
}
