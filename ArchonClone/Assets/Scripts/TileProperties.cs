using UnityEngine;
using System.Collections;

public class TileProperties : MonoBehaviour {

    public GameObject UnitOnTile = null;
    public GameObject UnitMoveController;
    public GameObject datTarget;
    public GameObject datNode;
    public GameObject AStarController;
    public GameObject SoundController;
    public bool fighting = false;
    public double fightTimer;
    public bool Occupied = false;
    public bool canPlace;
    
    // Use this for initialization
	void Start () {
        //UnitOnTile = null;
        UnitMoveController = GameObject.Find("MovementController");
        datNode = transform.FindChild("Node").gameObject;
        AStarController = GameObject.Find("ISOCamera");
        SoundController = GameObject.Find("UISoundController");
	}
	
	// Update is called once per frame
	void Update () {
        if(fighting)
        {
            if(fightTimer <= 3)
            {
                fightTimer += Time.deltaTime;
            }
            else
            {
                Debug.Log("GOTO fight Scene!!!");
                fighting = false; 
                fightTimer = 0;
                //Application.LoadLevel(6);
                
                if (GameObject.Find("EnemyTurnController") != null)
                {
                    Debug.Log("EnemyTurnController Found");
                    Application.LoadLevelAdditive("TutorialTestBattle");
                }
                else
                {
                    Application.LoadLevelAdditive("BattleTest");
                }
            }
        }
	}

    public void OnMouseOver()
    {
        if (UnitMoveController.GetComponent<PawnMove>().isMoving == false)
        {
            TurnStateMachine.OnHoverPiece = this.UnitOnTile;
            renderer.material.color = Color.yellow;
        }
        else if (UnitMoveController.GetComponent<PawnMove>().isMoving == true)
        {
            UnitMoveController.GetComponent<PawnMove>().MoveToTile = this.gameObject;
            if(Vector3.Distance(UnitMoveController.GetComponent<PawnMove>().SelectedPiece.transform.position, this.transform.position) <= UnitMoveController.GetComponent<PawnMove>().MaxMove)
            {
                renderer.material.color = Color.green;
                canPlace = true;
            }
            else
            {
                renderer.material.color = Color.red;
                canPlace = false; 
            }
        }

    }

    public void OnMouseExit()
    {
        if(this.tag == "WhiteTile")
        {
            renderer.material.color = Color.white;
        }
        if (this.tag == "BlackTile")
        {
            renderer.material.color = Color.black;
        }
        canPlace = false;
        UnitMoveController.GetComponent<PawnMove>().MoveToTile = null;
    }

    public void OnMouseDown()
    {
        Debug.Log("on mouse down called");
        if (canPlace == false && UnitOnTile != null)
        {
            SoundController.GetComponent<UISoundsScript>().playSelectPiece();
            if(TurnStateMachine.state == TurnStateMachine.State.playerTurn && TurnStateMachine.OnHoverPiece.tag == "White")
            {
                SelectPiece();
            }
            else if (TurnStateMachine.state == TurnStateMachine.State.otherTurn && TurnStateMachine.OnHoverPiece.tag == "Black")
            {
                SelectPiece();
            }
        }
        else if(canPlace == true && UnitOnTile == null)//these checks result in a piece moving to an empty hex tile
        {
            SoundController.GetComponent<UISoundsScript>().playMovePiece();
            if (UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().Occupied == false && canPlace == true)
            {
                if (UnitMoveController.GetComponent<PawnMove>().SelectedPiece.tag == "Black")
                {
                    //ReplaceBlackPiece(UnitMoveController.GetComponent<PawnMove>().SelectedPiece);
                    SetTarget();
                    if (UnitMoveController.GetComponent<PawnMove>().SelectedPiece.name == "BlackPawn(Clone)")
                    {
                        PiecePlaceScript.Black01Tile = UnitMoveController.GetComponent<PawnMove>().MoveToTile;
                        Debug.Log("SetBlackPawnTile");
                    }
                    else
                    {
                        PiecePlaceScript.Black02Tile = UnitMoveController.GetComponent<PawnMove>().MoveToTile;
                    }
                    TurnStateMachine.state = TurnStateMachine.State.playerTurn;
                }
                else
                {
                    if(GameObject.Find("EnemyTurnController") != null)
                    {
                        SoundController.GetComponent<UISoundsScript>().playError();
                    }
                    else
                    {
                        //ReplaceWhitePiece(UnitMoveController.GetComponent<PawnMove>().SelectedPiece);
                        SetTarget();
                        if (UnitMoveController.GetComponent<PawnMove>().SelectedPiece.name == "WhitePawn(Clone)")
                        {
                            PiecePlaceScript.White01Tile = UnitMoveController.GetComponent<PawnMove>().MoveToTile;
                        }
                        else
                        {
                            PiecePlaceScript.White02Tile = UnitMoveController.GetComponent<PawnMove>().MoveToTile;
                        }
                        TurnStateMachine.state = TurnStateMachine.State.otherTurn;
                    }
                }
            }
            UnitMoveController.GetComponent<PawnMove>().isMoving = false;
        }
        else if(canPlace == true && UnitOnTile != null)
        {
            SoundController.GetComponent<UISoundsScript>().playFight();
            if(UnitMoveController.GetComponent<PawnMove>().SelectedPiece.tag == "White")
            {
                if(UnitOnTile.tag == "Black" && canPlace)
                {
                    if(GameObject.Find("EnemyTurnConroller") != null)
                    {
                        if (UnitOnTile.name == "Black02(Clone)")
                        {
                        Debug.Log("Fight");
                        UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
                        GridManager.rescan = true;
                        UnitMoveController.GetComponent<PawnMove>().Player01 = UnitMoveController.GetComponent<PawnMove>().SelectedPiece;
                        UnitMoveController.GetComponent<PawnMove>().Player02 = UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().UnitOnTile;
                        fighting = true;
                        SetTarget();
                        }
                        else
                        {
                            SoundController.GetComponent<UISoundsScript>().playError();
                        }
                    }
                    else
                    {
                        Debug.Log("Fight");
                        UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
                        GridManager.rescan = true;
                        UnitMoveController.GetComponent<PawnMove>().Player01 = UnitMoveController.GetComponent<PawnMove>().SelectedPiece;
                        UnitMoveController.GetComponent<PawnMove>().Player02 = UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().UnitOnTile;
                        fighting = true;
                        SetTarget();
                    }
                }
            }
            else//if selectedPiece tag is black
            {
                if (UnitOnTile.tag == "White" && canPlace)
                {
                    Debug.Log("Fight");
                    UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
                    GridManager.rescan = true;
                    UnitMoveController.GetComponent<PawnMove>().Player01 = UnitMoveController.GetComponent<PawnMove>().SelectedPiece;
                    UnitMoveController.GetComponent<PawnMove>().Player02 = UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().UnitOnTile;
                    fighting = true; 
                    SetTarget();
                }
            }
        }
        else
        {
            SoundController.GetComponent<UISoundsScript>().playError();
        }
    }

    void ReplaceWhitePiece(GameObject selectedPiece)
    {
        Destroy(selectedPiece.gameObject);
        this.UnitOnTile = Instantiate(selectedPiece, this.transform.position, Quaternion.Euler(0, -90, 0)) as GameObject;
        this.Occupied = false;
        UnitMoveController.GetComponent<PawnMove>().isMoving = false;
        canPlace = false; 
    }
    void ReplaceBlackPiece(GameObject selectedPiece)
    {
        Destroy(selectedPiece.gameObject);
        this.UnitOnTile = Instantiate(selectedPiece, this.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;
        this.Occupied = false; 
        UnitMoveController.GetComponent<PawnMove>().isMoving = false;
        canPlace = false; 
    }
    void SetTarget()
    {
        GameObject newTarget = Instantiate(datTarget, transform.position, transform.rotation) as GameObject;
        UnitMoveController.GetComponent<PawnMove>().SelectedPiece.GetComponent<pieceMove>().targetPosition = newTarget.transform.position;
        UnitMoveController.GetComponent<PawnMove>().SelectedPiece.GetComponent<pieceMove>().GetNewPath();
        UnitMoveController.GetComponent<PawnMove>().SelectedPiece.GetComponent<pieceMove>().isMoving = true;
        Destroy(newTarget.gameObject);
        this.UnitOnTile = UnitMoveController.GetComponent<PawnMove>().SelectedPiece;
        //this.datNode.gameObject.SetActive(true);
        UnitMoveController.GetComponent<PawnMove>().currentTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
        UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(false);
        UnitMoveController.GetComponent<PawnMove>().currentTile.GetComponent<TileProperties>().UnitOnTile = null;
        UnitMoveController.GetComponent<PawnMove>().currentTile.GetComponent<TileProperties>().Occupied = false;
        UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().Occupied = false;
        UnitMoveController.GetComponent<PawnMove>().isMoving = false;
        canPlace = false;
        datNode.gameObject.SetActive(true);
    }

    void SelectPiece()
    {
        if (GameObject.Find("EnemyTurnController") != null)
        {
            if(TurnStateMachine.OnHoverPiece.name == "WhitePawn(Clone)")
            {
                UnitMoveController.GetComponent<PawnMove>().SelectedPiece = UnitOnTile;
                UnitMoveController.GetComponent<PawnMove>().isMoving = true;
                UnitMoveController.GetComponent<PawnMove>().currentTile = this.gameObject;
                UnitMoveController.GetComponent<PawnMove>().currentTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
                datNode.gameObject.SetActive(true);
                if (UnitMoveController.GetComponent<PawnMove>().isMoving)
                {
                    GridManager.rescan = true;
                }
            }
            else
            {
                SoundController.GetComponent<UISoundsScript>().playError();
            }
        }
        else
        {
            UnitMoveController.GetComponent<PawnMove>().SelectedPiece = UnitOnTile;
            UnitMoveController.GetComponent<PawnMove>().isMoving = true;
            UnitMoveController.GetComponent<PawnMove>().currentTile = this.gameObject;
            UnitMoveController.GetComponent<PawnMove>().currentTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
            datNode.gameObject.SetActive(true);
            if (UnitMoveController.GetComponent<PawnMove>().isMoving)
            {
                GridManager.rescan = true;
            }
        }
    }
}

