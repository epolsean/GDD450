using UnityEngine;
using System.Collections;

public class TileProperties : MonoBehaviour {

    public GameObject UnitOnTile = null;
    public GameObject UnitMoveController;
    public GameObject datTarget;
    public GameObject datNode;
    public GameObject AStarController;

    public bool Occupied = false;
    public bool canPlace;
	// Use this for initialization
	void Start () {
        //UnitOnTile = null;
        UnitMoveController = GameObject.Find("MovementController");
        datNode = transform.FindChild("Node").gameObject;
        AStarController = GameObject.Find("ISOCamera");
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void OnMouseOver()
    {
        if (UnitMoveController.GetComponent<PawnMove>().isMoving == false)
        {
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

    void OnMouseExit()
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

    void OnMouseDown()
    {
        if (canPlace == false && UnitOnTile != null)
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
        else if(canPlace == true && UnitOnTile == null)
        {
            if (UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().Occupied == false && canPlace == true)
            {
                if (UnitMoveController.GetComponent<PawnMove>().SelectedPiece.tag == "BlackPawn" || UnitMoveController.GetComponent<PawnMove>().SelectedPiece.tag == "Black02")
                {
                    //ReplaceBlackPiece(UnitMoveController.GetComponent<PawnMove>().SelectedPiece);
                    SetTarget();
                }
                else
                {
                    //ReplaceWhitePiece(UnitMoveController.GetComponent<PawnMove>().SelectedPiece);
                    SetTarget();
                }
            }
            UnitMoveController.GetComponent<PawnMove>().isMoving = false;
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
    }
}

