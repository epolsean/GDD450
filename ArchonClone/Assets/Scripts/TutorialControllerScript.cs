using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialControllerScript : MonoBehaviour {

    public int TutorialStepCout = 0;
    public GameObject OutPutText;
    public GameObject OutPutTextPanel;
    public GameObject TurnController;
    public GameObject SoundController;
    public GameObject EnemyPiece;
    public GameObject EnemyPieceStartTile; 
    public GameObject EnemyTile01; 
    public string IntroText = "Welcome to the Strategix boot camp! Here you will be transformed from the inferior being that you are, into a strategy machine! For this simulation, you will be in command of a group of the Sythetic units on the left!";
    public string tutorialText01 = "Stretegix is a turned based strategy game and this combat simulation will start with your opponent making the first move!";
    public string tutorialText02 = "Now it is your turn! You can hover over tiles and pieces to view their respective stats. Click the piece you wish to move with your left mouse button to select it. Select your synthetic Grunt piece Now!";
    public string tutorialText03 = "Very good soldier! when you have selected a piece you can now hover over tiles to select a tile to move to or an enemy to attack. You may also deselect your current selected piece by left clicking on it a second time. Deselect your synthetic Grunt now and select your Synthetic Tank piece NOW!";
    public string tutorialText04 = "Now is the time to go on the offensive! But be careful! Tiles give the faction pieces they belong to an additional boost of power when fighting on that tile. You may capture atile by moving your piece onto an enemy or neutral tile! Now Attack your opponents organic Runner piece!";
    public string tutorialText05 = "Congratulations Recruit! You now have the knowledge you'll need to venture forth and take control of more zones of this planet! Good Luck recruit!";

    // Use this for initialization
	void Start () {
        TurnController.GetComponent<OnTurnActions>().CanClick = false;
         
	}
	
	// Update is called once per frame
	void Update () {
        if(TutorialStepCout == 3)
        {
            //TutorialStepCout++;
            OutPutText.GetComponent<Text>().text = tutorialText02;
            OutPutTextPanel.SetActive(true);
            Invoke("TurnOnPanel", 1 / 10);
        }
        else if(TutorialStepCout == 6)
        {
            //TutorialStepCout++;
            OutPutText.GetComponent<Text>().text = tutorialText03;
            Invoke("TurnOnPanel", 1 / 10);
        }
        else if (TutorialStepCout == 9)
        {
            //TutorialStepCout++;
            OutPutText.GetComponent<Text>().text = tutorialText04;
            Invoke("TurnOnPanel", 1 / 10);
        }
        else if(TutorialStepCout == 12)
        {
            //TutorialStepCout++;
            OutPutText.GetComponent<Text>().text = tutorialText05;
            Invoke("TurnOnPanel", 1 / 10);
        }
        
        if(Input.GetMouseButtonDown(0))
        {
            if (TutorialStepCout == 0)
            {
                TutorialStepCout++;
                OutPutText.GetComponent<Text>().text = tutorialText01;
                EnemyPieceStartTile.GetComponent<OnTileActions>().TileNode.SetActive(true);
                GridManager.rescan = true;
                EnemyPiece.GetComponent<pieceMovementScript>().startMove = true; 
            }
            else if(TutorialStepCout == 1)//close tutorial Panel
            {
                TutorialStepCout++;
                EnemyPiece.GetComponent<pieceMovementScript>().datTile.GetComponent<OnTileActions>().TileNode.SetActive(true);
                GridManager.rescan = true; 
                OutPutTextPanel.SetActive(false);
                if (TurnController != null)
                {
                    TurnController.GetComponent<OnTurnActions>().CurrentTile = EnemyPieceStartTile; 
                    EnemyPieceStartTile.GetComponent<OnTileActions>().TileNode.SetActive(true);
                    GridManager.rescan = true;
                    //TurnController.GetComponent<OnTurnActions>().SelectedPiece = EnemyPiece;
                    //TurnController.GetComponent<OnTurnActions>().SelectedPiece.GetComponent<pieceMovementScript>().targetPosition = EnemyTile01.transform.position;
                    EnemyPiece.transform.position = new Vector3(EnemyPiece.transform.position.x, 0.5f, EnemyPiece.transform.position.z);
                    EnemyPiece.GetComponent<pieceMovementScript>().isMoving = true; 
                    EnemyPiece.GetComponent<pieceMovementScript>().targetPosition = EnemyTile01.transform.position;
                    EnemyPiece.GetComponent<pieceMovementScript>().GetNewPath();
                    EnemyPieceStartTile.GetComponent<OnTileActions>().PieceOnTile = null; 
                    EnemyTile01.GetComponent<OnTileActions>().PieceOnTile = EnemyPiece;
                    //TurnController.GetComponent<OnTurnActions>().SetTarget(EnemyTile01);
                }
            }
            //TutorialStepCount == 2 will set the panel to on

            else if(TutorialStepCout == 3)
            {
                TutorialStepCout++;
                OutPutTextPanel.SetActive(false);
                Invoke("TurnOnClick", 1 / 10); 
            }
            else if(TutorialStepCout == 4)//
            {
                TutorialStepCout++;
                OutPutTextPanel.SetActive(false);
                Invoke("TurnOnClick", 1 / 10); 
            }
            else if(TutorialStepCout == 7)
            {
                TutorialStepCout++;
                OutPutTextPanel.SetActive(false);
                Invoke("TurnOnClick", 1 / 10);
            }
            else if (TutorialStepCout == 10)
            {
                TutorialStepCout++;
                OutPutTextPanel.SetActive(false);
                Invoke("TurnOnClick", 1 / 10);
            }
            else if(TutorialStepCout >= 13)
            {
                PlayerPrefs.SetInt("TutorialComplete", 1);
                Application.LoadLevel("StartScreen");
            }
        }
	}

    public void TurnOnClick()
    {
        TurnController.GetComponent<OnTurnActions>().CanClick = true;
    }

    public void TurnOnPanel()
    {
        TutorialStepCout++;
        OutPutTextPanel.SetActive(true);
    }
}
