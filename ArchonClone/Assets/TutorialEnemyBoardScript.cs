using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialEnemyBoardScript : MonoBehaviour {

    public GameObject B01Turn0Tile;
    public GameObject B02Turn0Tile;
    public GameObject Turn01Tile;
    public GameObject Turn02Tile;

    public GameObject TutorialDummy;

    public GameObject datTarget;

    public GameObject StepButton;

    public GameObject StepText;

    public GameObject UnitMovementController;

    public double enemyTurn = 0;
	
    // Use this for initialization
	void Start () {
        enemyTurn = 0.5;
        UnitMovementController = GameObject.Find("MovementController");
        TurnStateMachine.state = TurnStateMachine.State.otherTurn;
        StepText.GetComponent<Text>().text = "Here is the Hex Tile grid of the field, the enemy will start by moving one of his pieces";
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Space))
        {
            NextStep();
        }
        
        if(enemyTurn == 1/* && TurnStateMachine.state == TurnStateMachine.State.otherTurn*/)
        {
            TutorialDummy = GameObject.Find("BlackGrunt(Clone)");
            UnitMovementController.GetComponent<PawnMove>().MoveToTile = Turn01Tile;
            UnitMovementController.GetComponent<PawnMove>().SelectedPiece = TutorialDummy;

            GameObject newTarget = Instantiate(datTarget, Turn01Tile.transform.position, Turn01Tile.transform.rotation) as GameObject;
            TutorialDummy.GetComponent<pieceMove>().targetPosition = newTarget.transform.position;
            TutorialDummy.GetComponent<pieceMove>().GetNewPath();
            //TutorialDummy.GetComponent<pieceMove>().isMoving = true;
            Destroy(newTarget.gameObject);
            Turn01Tile.GetComponent<TileProperties>().UnitOnTile = TutorialDummy;
            //this.datNode.gameObject.SetActive(true);
            //UnitMovementController.GetComponent<PawnMove>().currentTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
            //UnitMovementController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(false);
            B02Turn0Tile.GetComponent<TileProperties>().UnitOnTile = null;
            B02Turn0Tile.GetComponent<TileProperties>().Occupied = false;
            Turn01Tile.GetComponent<TileProperties>().Occupied = true;
            UnitMovementController.GetComponent<PawnMove>().isMoving = false;
            Turn01Tile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
            enemyTurn += 0.5;
            TurnStateMachine.state = TurnStateMachine.State.playerTurn;
            StepText.GetComponent<Text>().text = "The Enemy is in a perfect place for you to attack! Select Your Tank piece with the mouse then select the enemies piece!";
            
        }
        else if(enemyTurn == 2)
        {
            StepButton.active = false;
        }
        else if(enemyTurn == 3)
        {
            StepButton.active = false; 
        }
        /*else if(enemyTurn == 4)
        {
            TutorialDummy = GameObject.Find("BlackPawn(Clone)");
            UnitMovementController.GetComponent<PawnMove>().MoveToTile = Turn02Tile;
            UnitMovementController.GetComponent<PawnMove>().SelectedPiece = TutorialDummy;

            GameObject newTarget = Instantiate(datTarget, Turn02Tile.transform.position, Turn02Tile.transform.rotation) as GameObject;
            TutorialDummy.GetComponent<pieceMove>().targetPosition = newTarget.transform.position;
            TutorialDummy.GetComponent<pieceMove>().GetNewPath();
            //TutorialDummy.GetComponent<pieceMove>().isMoving = true;
            Destroy(newTarget.gameObject);
            Turn01Tile.GetComponent<TileProperties>().UnitOnTile = TutorialDummy;
            //this.datNode.gameObject.SetActive(true);
            //UnitMovementController.GetComponent<PawnMove>().currentTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
            //UnitMovementController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(false);
            B01Turn0Tile.GetComponent<TileProperties>().UnitOnTile = null;
            B01Turn0Tile.GetComponent<TileProperties>().Occupied = false;
            Turn02Tile.GetComponent<TileProperties>().Occupied = true;
            UnitMovementController.GetComponent<PawnMove>().isMoving = false;
            //Turn02Tile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
            enemyTurn += 0.5;
            TurnStateMachine.state = TurnStateMachine.State.playerTurn;
        }*/
        else if(enemyTurn == 4.5)
        {
            StepText.GetComponent<Text>().text = "Congratulations! You Have successfully Destroyed the enemies piece! Now you have the required Knowledge to continue your conquest of this planet!";
            StepButton.active = true;
        }
        else if(enemyTurn == 5)
        {
            Application.LoadLevel("StartScreen");
        }
	}

    public void NextStep()
    {
        if(StepButton.active)
            enemyTurn += .5;
    }
}
