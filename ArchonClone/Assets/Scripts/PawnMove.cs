using UnityEngine;
using System.Collections;

public class PawnMove : MonoBehaviour {

    public GameObject SelectedPiece;
    public GameObject currentTile;
    public bool isMoving = false;
    public GameObject MoveToTile;
    public GameObject Player01;
    public GameObject Player02;
    public double MaxMove;
    public float MoveRange;

    public GameObject StatPan;
    public GameObject BlackGruntPan;
    public GameObject BlackTankPan;
    public GameObject BlackRunnerPan;
    public GameObject BlackScoutPan;
    public GameObject WhiteScoutPan;
    public GameObject WhiteTankPan;
    public GameObject WhiteGruntPan;
    public GameObject WhiteRunnerPan;

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
            if (SelectedPiece.name == "WhiteTank(Clone)" || SelectedPiece.name == "BlackTank(Clone)")
            {
                MaxMove = 5 * 2;
                MoveRange = 8;
            }
            else if(SelectedPiece.name == "WhiteGrunt(Clone)"|| SelectedPiece.name == "BlackGrunt(Clone)")
            {
                MaxMove = 5 * 2.75;
                MoveRange = 11;
            }
            else if (SelectedPiece.name == "WhiteRunner(Clone)" || SelectedPiece.name == "BlackRunner(Clone)")
            {
                MaxMove = 5 * 3.5;
                MoveRange = 16;
            }
            else if(SelectedPiece.name == "WhiteScout(Clone)" || SelectedPiece.name == "BlackScout(Clone)")
            {
                MaxMove = 5 * 2.75;
                MoveRange = 11;
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
