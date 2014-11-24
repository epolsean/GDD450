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
        enemyTurn = 0.0;
        UnitMovementController = GameObject.Find("MovementController");
        TurnStateMachine.state = TurnStateMachine.State.otherTurn;
        StepText.GetComponent<Text>().text = "Welcome to the Strategix boot camp! Here you will be transformed from the inferior being that you are, into a strategy machine! For this simulation, you will be in command of a group of the Sythetic units on the left!";
        B01Turn0Tile.GetComponent<TileProperties>().datNode.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.R))
        {
            //StepButton.SetActive(false);
            NextStep();
        }
        if(enemyTurn == 0.5)
        {
            //introduce UI to player
            StepText.GetComponent<Text>().text = "In front of you is the Hex Tile Grid, this is your strategic field that displays both players units they control. There are unit counters at the two corners of the screen to display both players units that are still in play.";
            StepButton.SetActive(true);
        }
        else if(enemyTurn == 1)
        {
            //introduce strategy controls
            StepText.GetComponent<Text>().text = "To select a unit on your turn, simply move your cursor over one of your units and press the mouse button. You may select a different piece to move by selecting a different piece you control.";
            StepButton.SetActive(true);
        }
        else if(enemyTurn == 1.5)
        {
            //enemy moves first text
            StepText.GetComponent<Text>().text = "Your opponent will go first by moving a grunt unit. You can identify a units name and stats by hovering over the tile it occupies with your cursor.";
            StepButton.SetActive(true);
        }
        else if(enemyTurn == 2)
        {
            //enemy move
            StepButton.SetActive(false);
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
        else if(enemyTurn == 3)
        {
            //select your tank
            StepText.GetComponent<Text>().text = "Now it is your turn! Select your Tank unit, you may notice that the tile you hover over will change colors depending if the move is possible; Green for good, Red for bad.";
            StepButton.SetActive(true);
        }
        else if(enemyTurn == 3.5)
        {
            StepButton.SetActive(false);
        }
        else if(enemyTurn == 4)
        {
            //select enemy piece to attack
            StepText.GetComponent<Text>().text = "Now select your opponent's Grunt unit that is now within your Tank units movement range.";
            StepButton.SetActive(true);
        }
        else if(enemyTurn == 4.5)
        {
            StepButton.SetActive(false);
        }
        else if(enemyTurn == 5)
        {
            //combas phase
        }
        else if(enemyTurn == 5.5)
        {
            //end tutorial
            StepText.GetComponent<Text>().text = "Congratulations! You Have successfully Destroyed the enemy's Grunt! This concludes the tutorial! Use what you have learned here to continue your Conquest!";
            StepButton.SetActive(true);
        }
        else if(enemyTurn == 6)
        {
            Application.LoadLevel("StartScreen");
        }
        
        /*
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
        }*/
	}

    public void NextStep()
    {
        if(StepButton.activeSelf)
            enemyTurn += .5;
    }

    public void SetTutorialTextTrue()
    {
        Turn01Tile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
        enemyTurn += .5;
    }
    
}
