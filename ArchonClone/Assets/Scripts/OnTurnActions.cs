using UnityEngine;
using System.Collections;
using Pathfinding;

/* this script will contain all methods and variables 
 * used when moving pieces and attacking during a turn, 
 * it will call A* to start a piece moving and also test 
 * the distance to the target tile between the current 
 * tile to check if it is a possible legal move. 
 */

public class OnTurnActions : MonoBehaviour
{

    public Vector3 targetPosition;
    public CharacterController pieceCharacterController;
    public int MaxPathNodes;
    public float MaxMove;
    public GameObject SoundController;
    public GameObject MoveToTile;
    public GameObject OnHoverTile;
    public GameObject OnHoverPiece;
    public GameObject CurrentTile;
    public GameObject SelectedPiece;
    public GameObject PieceStatPanel;
    public GameObject HealthPanel;
    public GameObject DamagePanel;
    public GameObject SpeedPanel; 
    public bool hasSelectedPiece = false;
    //Pathfinder variables
    public Seeker seeker;
    public Path path;
    public float nextWaypointDistance = 2f;
    public int currentWaypoint = 0;
    public bool isGenPath = false;
    public bool drawnPath = false;
    public GameObject[] allTiles;



    // Use this for initialization
    void Start()
    {
        allTiles = GameObject.FindGameObjectsWithTag("Tile");
        SoundController = GameObject.Find("UISoundController");
        PieceStatPanel = GameObject.Find("PieceStatPan2.0");
        //PieceStatPanel.SetActive(false);
        HealthPanel = GameObject.Find("HealthSlider");
        DamagePanel = GameObject.Find("DamageSlider");
        SpeedPanel = GameObject.Find("SpeedSlider"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (OnHoverTile != null)
            {
                if (hasSelectedPiece == false)//before player has selected a piece
                {
                    //check if piece is current players

                    if (OnHoverTile != null && OnHoverTile.GetComponent<OnTileActions>().PieceOnTile != null)
                    {
                        if (TurnStateMachine.state == TurnStateMachine.State.playerTurn && OnHoverTile.GetComponent<OnTileActions>().PieceOnTile.tag == "White")
                        {
                            SelectPiece(OnHoverPiece);
                        }
                        else if (TurnStateMachine.state == TurnStateMachine.State.otherTurn && OnHoverTile.GetComponent<OnTileActions>().PieceOnTile.tag == "Black")
                        {
                            SelectPiece(OnHoverPiece); 
                        }
                        else
                        {
                            SoundController.GetComponent<UISoundsScript>().playError(); 
                        }
                        
                    }
                }
                else
                {
                    if (CurrentTile == OnHoverTile)
                    {
                        ResetController();
                        SoundController.GetComponent<UISoundsScript>().playDeselect();  
                    }
                    else
                    {
                        if (SelectedPiece.GetComponent<pieceMovementScript>().path.vectorPath.Count <= SelectedPiece.GetComponent<pieceMovementScript>().MaxPathNodes)
                        {
                            if(OnHoverTile.GetComponent<OnTileActions>().PieceOnTile == null)
                            {
                                SetTarget(OnHoverTile);
                            }
                            else//brackets check when you are initiating combat
                            {
                                if(OnHoverTile.GetComponent<OnTileActions>().PieceOnTile.tag != SelectedPiece.tag)//this check initiates combat
                                {
                                    SoundController.GetComponent<UISoundsScript>().playFight();
                                    OnHoverTile.GetComponent<OnTileActions>().AtkPiece = SelectedPiece; 
                                }
                            }
                            
                        }
                        else
                        {
                            SoundController.GetComponent<UISoundsScript>().playError();
                            //ResetController();
                        }
                    }
                }
            }
        }
        /*if(OnHoverPiece != null)
        {
            //print("there's something there!!!"); 
            //if(PieceStatPanel != null)
            //{
                
                PieceStatPanel.SetActive(true); 
            //}
        }
        else
        {
            PieceStatPanel.SetActive(false); 
        }*/
    }

    /* 
     * called when you assign a selected piece to move
     * it assigns the selectedPiece, MaxMove, CurrentTile, 
     * MaxPathNodes
     */
    void SelectPiece(GameObject Piece)
    {
        print("SelectPiece Called");
        hasSelectedPiece = true;
        print("bool set to true");
        SelectedPiece = Piece;
        CurrentTile = OnHoverTile;
        MaxPathNodes = Piece.GetComponent<pieceMovementScript>().MaxPathNodes; 
        //MaxMove = SelectedPiece.GetComponent<PiecePropScript>()
        //MaxPathNodes = SelectedPiece.GetComponent<PiecePropScript>()
        OnHoverTile.GetComponent<OnTileActions>().isSelected = true;
        OnHoverTile.renderer.material.color = Color.yellow;
        if (SoundController != false)
        {
            SoundController.GetComponent<UISoundsScript>().playSelectPiece();
        }


    }

    /*
     * called when you move a piece to an opponent tile with a piece
     * occupying it, this will start combat and show the player which
     * piece won that battle.
     */
    public void StartCombat()
    {

    }

    /*
     * when called the TurnController is reset to factory 
     * fresh settings this will be called after you move 
     * your piece or when you deselect your piece
     */
    public void ResetController()
    {
        print("ResetController Called");
        
        CurrentTile.renderer.material.color = Color.white;
        CurrentTile.GetComponent<OnTileActions>().isSelected = false;
        //MoveToTile.renderer.material.color = Color.white;
        //MoveToTile.GetComponent<OnTileActions>().isSelected = false;
        SelectedPiece = null;
        CurrentTile = null;
        MoveToTile = null;
        MaxMove = 0;
        MaxPathNodes = 0;
        hasSelectedPiece = false;
        resetAllTiles();
        Camera.main.GetComponent<CameraZoomController>().ResetTransform();

    }

    /*
     * EndOfTurn is obviously called at the end of a turn, it sets the 
     * variables for the tiles like if there is a piece on it and which 
     * piece is on it. It also resets the oldTile to a plain old tile.
     * This also calls NextTurn() to change who's turn it is anyway!
     */ 
    public void EndOfTurn()
    {
        CurrentTile.GetComponent<OnTileActions>().PieceOnTile = null;
        MoveToTile.GetComponent<OnTileActions>().PieceOnTile = SelectedPiece;
        MoveToTile.GetComponent<OnTileActions>().isSelected = false;
        NextTurn(); 
    }

    /*
     * called to progress to the next players turn and that's pretty much it!
     */ 
    public void NextTurn()
    {
        if(TurnStateMachine.state == TurnStateMachine.State.playerTurn)
        {
            TurnStateMachine.state = TurnStateMachine.State.otherTurn; 
        }
        else
        {
            TurnStateMachine.state = TurnStateMachine.State.playerTurn; 
        }
    }

    void SetTarget(GameObject targetTile)//called when you select a tile to move to
    {
        SelectedPiece.GetComponent<pieceMovementScript>().isMoving = true;
        SelectedPiece.GetComponent<pieceMovementScript>().startMove = true;
        targetTile.GetComponent<OnTileActions>().isSelected = true;
        MoveToTile = targetTile;
        OnHoverTile.renderer.material.color = Color.green;
        hasSelectedPiece = false;
        SelectedPiece.transform.position = new Vector3(SelectedPiece.transform.position.x, 0.5f, SelectedPiece.transform.position.z);//this is a quick fix for a weird bug where pice was being clocked from movng
        if (SoundController != false)
        {
            SoundController.GetComponent<UISoundsScript>().playMovePiece();
        }
        Camera.main.GetComponent<CameraZoomController>().FollowTarget(SelectedPiece);
    }

    public void GenPath()//called to generated a path from the selectedPiece in the TurnController and the OnHoverTile(the tile you are hovering over)
    {
        print("Generated Path");
        SelectedPiece.GetComponent<pieceMovementScript>().targetPosition = OnHoverTile.transform.position;
        //SelectedPiece.GetComponent<pieceMovementScript>().GenNewPath();
        SelectedPiece.GetComponent<pieceMovementScript>().GetNewPath();
        isGenPath = true;
    }

    public void resetAllTiles()//primarily used to reset the highlighted tiles in a path, it sets all tiles on the board to a base color of white
    {
        for (int i = 0; i < allTiles.Length; i++)
        {
            allTiles[i].renderer.material.color = Color.white;
        }
    }

}
