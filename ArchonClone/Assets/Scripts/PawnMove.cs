using UnityEngine;
using System.Collections;

public class PawnMove : MonoBehaviour {

    public GameObject SelectedPiece;
    public bool isMoving = false;
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (SelectedPiece != null && SelectedPiece.tag == "White")
        {
            if (Input.GetKeyDown(KeyCode.D) && isMoving == false)
            {
                MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.A) && isMoving == false)
            {
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.W) && isMoving == false)
            {
                MoveUp();
            }
            if (Input.GetKeyDown(KeyCode.S) && isMoving == false)
            {
                MoveDown();
            }
        }
        if (SelectedPiece != null && SelectedPiece.tag == "Black")
        {
            if (Input.GetKeyDown(KeyCode.D) && isMoving == false)
            {
                MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.A) && isMoving == false)
            {
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.W) && isMoving == false)
            {
                MoveUp();
            }
            if (Input.GetKeyDown(KeyCode.S) && isMoving == false)
            {
                MoveDown();
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
