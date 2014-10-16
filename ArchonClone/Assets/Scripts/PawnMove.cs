using UnityEngine;
using System.Collections;

public class PawnMove : MonoBehaviour {

    public GameObject SelectedPiece;
    public GameObject currentTile;
    public bool isMoving = false;
    public GameObject MoveToTile;

    public double MaxMove;

    // Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isMoving = false;
            SelectedPiece = null;
        }
        if(isMoving && MoveToTile != null)
        {
            if (SelectedPiece.name == "WhitePawn(Clone)" || SelectedPiece.name == "BlackPawn(Clone)")
            {
                MaxMove = 5 * 2;
            }
            else if(SelectedPiece.name == "White02(Clone)"|| SelectedPiece.name == "Black02(Clone)")
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
