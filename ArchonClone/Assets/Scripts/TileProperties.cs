using UnityEngine;
using System.Collections;

public class TileProperties : MonoBehaviour {

    public GameObject UnitOnTile = null;
    public GameObject UnitMoveController;

    public bool Occupied = false;
    public bool canPlace;
	// Use this for initialization
	void Start () {
        //UnitOnTile = null;
        UnitMoveController = GameObject.Find("MovementController");
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnMouseOver()
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
    }

    void OnMouseDown()
    {
        if (canPlace == false && UnitOnTile != null)
        {
            UnitMoveController.GetComponent<PawnMove>().SelectedPiece = UnitOnTile;
            UnitMoveController.GetComponent<PawnMove>().isMoving = true;
        }
        else
        {
            if (UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().Occupied == false && canPlace == true)
            {
                if (UnitMoveController.GetComponent<PawnMove>().SelectedPiece.tag == "BlackPawn" || UnitMoveController.GetComponent<PawnMove>().SelectedPiece.tag == "Black02")
                {
                    ReplaceBlackPiece(UnitMoveController.GetComponent<PawnMove>().SelectedPiece);
                }
                else
                {
                    ReplaceWhitePiece(UnitMoveController.GetComponent<PawnMove>().SelectedPiece);
                }
            }
            UnitMoveController.GetComponent<PawnMove>().isMoving = false;
        }
    }

    void ReplaceWhitePiece(GameObject selectedPiece)
    {
        Destroy(selectedPiece.gameObject);
        this.UnitOnTile = Instantiate(selectedPiece, this.transform.position, this.transform.rotation) as GameObject;
        UnitMoveController.GetComponent<PawnMove>().isMoving = false;
        canPlace = false; 
    }
    void ReplaceBlackPiece(GameObject selectedPiece)
    {
        Destroy(selectedPiece.gameObject);
        this.UnitOnTile = Instantiate(selectedPiece, this.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;
        UnitMoveController.GetComponent<PawnMove>().isMoving = false;
        canPlace = false; 
    }
}

