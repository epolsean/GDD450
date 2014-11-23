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
        B01Turn0Tile.GetComponent<TileProperties>().datNode.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.R))
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
            TutorialDummy.GetComponent<pieceMove>().isMoving = true;
            Destroy(newTarget.gameObject);
            Turn01Tile.GetComponent<TileProperties>().UnitOnTile = TutorialDummy;
            //this.datNode.gameObject.SetActive(true);
            //UnitMovementController.GetComponent<PawnMove>().currentTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
            //UnitMovementController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(false);
            B02Turn0Tile.GetComponent<TileProperties>().UnitOnTile = null;
            B02Turn0Tile.GetComponent<TileProperties>().Occupied = false;
            Turn01Tile.GetComponent<TileProperties>().Occupied = true;
            UnitMovementController.GetComponent<PawnMove>().isMoving = false;
            //TurnStateMachine.state = TurnStateMachine.State.playerTurn;
            StepButton.SetActive(false);
            enemyTurn += 0.5;
            Invoke("SetTutorialTextTrue", 3);
            //SetTutorialTextTrue();
            
        }
        else if(enemyTurn == 2)
        {
            StepButton.SetActive(false);
        }
        else if(enemyTurn == 3)
        {
            StepButton.SetActive(false); 
        }
        else if(enemyTurn == 4.5)
        {
            StepText.GetComponent<Text>().text = "Congratulations! You Have successfully Destroyed the enemies piece! Now you have the required Knowledge to continue your conquest of this planet!";
            StepButton.SetActive(true);
        }
        else if(enemyTurn == 5)
        {
            Application.LoadLevel("StartScreen");
        }
	}

    public void NextStep()
    {
        if(StepButton.activeSelf)
            enemyTurn += .5;
    }

    public void SetTutorialTextTrue()
    {
        Turn01Tile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
        StepButton.SetActive(true);


        StepText.GetComponent<Text>().text = "When you hover over a tile that is occupied by a unit that pieces name and stats will be displayed to you. The enemy Grunt unit has moved to a tile within your Tank unit! You can view how far your selected unit can travel by selecting it with your left mouse button. When the tile shows green than that is a target within range, if red then target is out of range. Select your Tank unit and target the enemies Grunt unit with the left mouse button to ATTACK!";
    }
    
}
