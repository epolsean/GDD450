using UnityEngine;
using System.Collections;

public class PlayerTutorialControl : MonoBehaviour {

    public GameObject TutorialEnemyController;
    public GameObject UnitMoveController;
    
    // Use this for initialization
	void Start () {
        UnitMoveController = GameObject.Find("MovementController");
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(0))
        {
            if (/*TurnStateMachine.state == TurnStateMachine.State.playerTurn && */TutorialEnemyController.GetComponent<TutorialEnemyBoardScript>().enemyTurn == 0)
            {
                Invoke("SetEnemyTurn", 3);
                //TurnStateMachine.state = TurnStateMachine.State.otherTurn;
            }
            else if (TutorialEnemyController.GetComponent<TutorialEnemyBoardScript>().enemyTurn == 2)
            {
                TutorialEnemyController.GetComponent<TutorialEnemyBoardScript>().enemyTurn = 0;
            }
        }
	}

    void SetEnemyTurn()
    {
        TutorialEnemyController.GetComponent<TutorialEnemyBoardScript>().enemyTurn = 3;
    }
}
