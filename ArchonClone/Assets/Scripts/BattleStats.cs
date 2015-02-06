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

    public enum GameType { Elimination, Domination, ControlPoints };

    public static GameType currentGameType;

    public void SetGameElimination()
    {
        currentGameType = GameType.Elimination;
    }

    public void SetGameDomination()
    {
        currentGameType = GameType.Domination;
    }

    public void SetGameControlPoints()
    {
        currentGameType = GameType.ControlPoints;
    }
}
