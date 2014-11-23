using UnityEngine;
using System.Collections;

public class Accelerator : MonoBehaviour 
{

    GameObject player1;
    GameObject player2;

    public static float countdownTimer;
	public GameObject MoveController;

    void Start()
    {
        MoveController = GameObject.Find("MovementController");
        countdownTimer = 35f;
        player1 = GameObject.Find("Player1(Clone)");
        if (BattleStats.singlePlayer)
        {
            player2 = GameObject.Find("EnemyAI(Clone)");
        }
        else
        {
            player2 = GameObject.Find("Player2(Clone)");
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (MoveController.GetComponent<PawnMove>().Player02 != null && MoveController.GetComponent<PawnMove>().Player01 != null)
        {
            if (player1 == null)
            {
                player1 = GameObject.Find("Player1(Clone)");
            }
            if (BattleStats.singlePlayer)
            {
                if (player2 == null)
                {
                    player2 = GameObject.Find("EnemyAI(Clone)");
                }
            }
            else
            {
                if (player2 == null)
                {
                    player2 = GameObject.Find("Player2(Clone)");
                }
            }
            if (countdownTimer <= 0)
            {
                if (player1 != null)
                {
                    player1.GetComponent<Player1MovementController>().health -= 2 * Time.deltaTime;
                }
                if (BattleStats.singlePlayer && player2 != null)
                {
                    player2.GetComponent<EnemyAIForBattle>().health -= 2 * Time.deltaTime;
                }
                else if (!BattleStats.singlePlayer && player2 != null)
                {
                    player2.GetComponent<Player2MovementController>().health -= 2 * Time.deltaTime;
                }
            }
            else
            {
                countdownTimer -= Time.deltaTime;
            }
        }
	}
}
