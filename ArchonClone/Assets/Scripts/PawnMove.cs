using UnityEngine;
using System.Collections;

public class PawnMove : MonoBehaviour {

    public GameObject SelectedPiece;
    public bool isMoving = false;
    public GameObject MoveToTile;

    public double MaxMove;

    // Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        /*if (SelectedPiece != null && SelectedPiece.tag == "White")
        {
            if (Input.GetKeyDown(KeyCode.D) && isMoving)
            {
                MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.A) && isMoving)
            {
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.W) && isMoving)
            {
                MoveUp();
            }
            if (Input.GetKeyDown(KeyCode.S) && isMoving)
            {
                MoveDown();
            }
        }
        if (SelectedPiece != null && SelectedPiece.tag == "Black")
        {
            if (Input.GetKeyDown(KeyCode.D) && isMoving)
            {
                MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.A) && isMoving)
            {
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.W) && isMoving)
            {
                MoveUp();
            }
            if (Input.GetKeyDown(KeyCode.S) && isMoving)
            {
                MoveDown();
            }
        }*/
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isMoving = false;
            SelectedPiece = null;
        }
        if(isMoving && MoveToTile != null)
        {
            if (SelectedPiece.tag == "WhitePawn" || SelectedPiece.tag == "BlackPawn")
            {
                MaxMove = 5 * 2;
            }
            else if(SelectedPiece.tag == "White02"|| SelectedPiece.tag == "Black02")
            {
                MaxMove = 5 * 3;
            }
                
        }
	}

    void MoveUp()
    {
        SelectedPiece.transform.position = new Vector3(SelectedPiece.transform.position.x, SelectedPiece.transform.position.y, SelectedPiece.transform.position.z + 5);
    }
    void MoveDown()
    {
        SelectedPiece.transform.position = new Vector3(SelectedPiece.transform.position.x, SelectedPiece.transform.position.y, SelectedPiece.transform.position.z - 5);
    }
    void MoveLeft()
    {
        SelectedPiece.transform.position = new Vector3(SelectedPiece.transform.position.x - 5, SelectedPiece.transform.position.y, SelectedPiece.transform.position.z);
    }
    void MoveRight()
    {
        SelectedPiece.transform.position = new Vector3(SelectedPiece.transform.position.x + 5, SelectedPiece.transform.position.y, SelectedPiece.transform.position.z);
    }
}
