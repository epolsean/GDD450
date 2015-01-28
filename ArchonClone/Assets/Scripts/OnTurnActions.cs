using UnityEngine;
using System.Collections;
using Pathfinding;

/* this script will contain all methods and variables 
 * used when moving pieces and attacking during a turn, 
 * it will call A* to start a piece moving and also test 
 * the distance to the target tile between the current 
 * tile to check if it is a possible legal move. 
 */

public class OnTurnActions : MonoBehaviour {

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
    public bool hasSelectedPiece = false; 
    //Pathfinder variables
    public Seeker seeker;
    public Path path;
    public float nextWaypointDistance = 2f;
    public int currentWaypoint = 0;

     
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(0))
        {
            if(OnHoverTile != null)
            {
                if (hasSelectedPiece == false)
                {
                    //check if piece is current players
                    
                    if (OnHoverTile != null && OnHoverTile.GetComponent<OnTileActions>().PieceOnTile != null)
                    {
                        SelectPiece(OnHoverPiece);
                    }
                }
                else
                {
                    if (CurrentTile == OnHoverTile)
                    {
                        ResetController();
                    }
                }
            }
        }
	}

    /* called when you assign a selected piece to move
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
        //MaxMove = SelectedPiece.GetComponent<PiecePropScript>()
        //MaxPathNodes = SelectedPiece.GetComponent<PiecePropScript>()
        OnHoverTile.GetComponent<OnTileActions>().isSelected = true; 
        OnHoverTile.renderer.material.color = Color.yellow; 

        
    }
    /*when called the TurnController is reset to factory 
     * fresh settings this will be called after you move 
     * your piece or when you deselect your piece
     */ 
    void ResetController()
    {
        print("ResetController Called");
        CurrentTile.renderer.material.color = Color.white;
        CurrentTile.GetComponent<OnTileActions>().isSelected = false; 
        SelectedPiece = null;
        CurrentTile = null; 
        MaxMove = 0;
        MaxPathNodes = 0;
        hasSelectedPiece = false;
        
    }
    
    void SetTarget()
    {
        CurrentTile = OnHoverTile;
    }
}
