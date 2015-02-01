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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (OnHoverTile != null)
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
                    else
                    {
                        if (SelectedPiece.GetComponent<pieceMovementScript>().path.vectorPath.Count <= SelectedPiece.GetComponent<pieceMovementScript>().MaxPathNodes)
                        {
                            SetTarget(OnHoverTile);
                        }
                        else
                        {
                            SoundController.GetComponent<UISoundsScript>().playError();
                            ResetController();
                        }
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
        if (SoundController != false)
        {
            SoundController.GetComponent<UISoundsScript>().playSelectPiece();
        }


    }
    /*when called the TurnController is reset to factory 
     * fresh settings this will be called after you move 
     * your piece or when you deselect your piece
     */
    public void ResetController()
    {
        print("ResetController Called");
        CurrentTile.GetComponent<OnTileActions>().PieceOnTile = null;
        CurrentTile.renderer.material.color = Color.white;
        CurrentTile.GetComponent<OnTileActions>().isSelected = false;
        MoveToTile.GetComponent<OnTileActions>().PieceOnTile = SelectedPiece;
        MoveToTile.renderer.material.color = Color.white;
        MoveToTile.GetComponent<OnTileActions>().isSelected = false;
        SelectedPiece = null;
        CurrentTile = null;
        MoveToTile = null;
        MaxMove = 0;
        MaxPathNodes = 0;
        hasSelectedPiece = false;
        resetAllTiles();
        Camera.main.GetComponent<CameraZoomController>().ResetTransform();

    }

    void SetTarget(GameObject targetTile)//called when you select a tile to move to
    {
        SelectedPiece.GetComponent<pieceMovementScript>().isMoving = true;
        SelectedPiece.GetComponent<pieceMovementScript>().startMove = true;
        targetTile.GetComponent<OnTileActions>().isSelected = true;
        MoveToTile = targetTile;
        OnHoverTile.renderer.material.color = Color.green;
        hasSelectedPiece = false;
        SelectedPiece.transform.position = new Vector3(SelectedPiece.transform.position.x, 0.1f, SelectedPiece.transform.position.z);//this is a quick fix for a weird bug where pice was being clocked from movng
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
