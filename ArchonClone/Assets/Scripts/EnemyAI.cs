using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public enum State { Idle, SelectingPiece, DaMove, Ending }
    public static State AIstate;
    public GameObject AISelectedPiece;
    public GameObject MoveToTile;
    public GameObject CurrentTile;
    public GameObject AITargetPlayerP;
    public GameObject AIFinalTurnTarget;

    
    // Use this for initialization
	void Start () {
        NextState();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator IdleState()
    {
        Debug.Log("EnemyIdle: Enter");
        while (AIstate == State.Idle)
        {
            
            if(TurnStateMachine.state == TurnStateMachine.State.otherTurn)
            {
                AIstate = State.SelectingPiece;
            }
            else
            {
                print("Player's Turn");
            }
            yield return 0;
        }
        Debug.Log("EnemyIdle: Exit");
        if (Network.isClient || Network.isServer)
            networkView.RPC("NextState", RPCMode.AllBuffered);
        else
            NextState();
    }

    IEnumerator SelectingPieceState()//this state will determine if the enemy will attack a player or just select a random tile to move to
    {
       // Debug.Log("Enemy" + AIstate.ToString() + ": Exit");
        Debug.Log("Enemy SelectingPieceState: Enter");
        while (AIstate == State.SelectingPiece)
        {
            AIstate = State.DaMove;
            AISelectedPiece = SelectRandP("Black");//this will give you a random black piece
            AITargetPlayerP = FindNearestPiece(AISelectedPiece, "White");

            Debug.Log("Distance: " + Vector3.Distance(AISelectedPiece.transform.position, AITargetPlayerP.transform.position));

            if(Vector3.Distance(AISelectedPiece.transform.position, AITargetPlayerP.transform.position) <= AISelectedPiece.GetComponent<pieceMove>().MaxPieceMove)
            {
                Debug.Log("Player is Close enough to attack");
                AIFinalTurnTarget = AITargetPlayerP;
            }
            else
            {
                Debug.Log("Player out of range, finding random tile");
                AIFinalTurnTarget = FindTile(AISelectedPiece, "White");
            }
            //AISelectedPiece = SelectRandP("Black");
            //AITargetPlayerP = FindNearestPiece(AISelectedPiece, "White");
            //TurnStateMachine.state = TurnStateMachine.State.playerTurn;
            //AIstate = State.DaMove;
            //AIstate = State.Idle;
            //TurnStateMachine.state = TurnStateMachine.State.playerTurn;
            yield return 0;
        }
        //Debug.Log("Enemy" + AIstate.ToString() + ": Exit");
        Debug.Log("Enemy SelectingPieceState: Exit");
        if (Network.isClient || Network.isServer)
            networkView.RPC("NextState", RPCMode.AllBuffered);
        else
            NextState();
    }

    IEnumerator DaMoveState()//this state will determine if the enemy will attack a player or just select a random tile to move to
    {
        Debug.Log("Enemy" + AIstate.ToString() + ": Exit");
        while (AIstate == State.DaMove)
        {
            AIstate = State.Ending;
            SetTargetAI(AISelectedPiece, AIFinalTurnTarget);
            yield return 0;
        }
        Debug.Log("Enemy" + AIstate.ToString() + ": Exit");
        if (Network.isClient || Network.isServer)
            networkView.RPC("NextState", RPCMode.AllBuffered);
        else
            NextState();
    }

    IEnumerator EndingState()//this state will determine if the enemy will attack a player or just select a random tile to move to
    {
        Debug.Log("Enemy" + AIstate.ToString() + ": Exit");
        while (AIstate == State.Ending)
        {
            TurnStateMachine.state = TurnStateMachine.State.playerTurn;
            AIstate = State.Idle;
            yield return 0;
        }
        Debug.Log("Enemy" + AIstate.ToString() + ": Exit");
        if (Network.isClient || Network.isServer)
            networkView.RPC("NextState", RPCMode.AllBuffered);
        else
            NextState();
    }

    [RPC]
    void NextState()
    {
        string methodName = AIstate.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }

    public GameObject SelectRandP(string TagName)//selects a random piece from whichever side you specify with the TagName variable(i.e. Black/White...)
    {
        GameObject selectedPiece = null;
        //collect all pieces with given tag...

        GameObject[] AIPs;
        AIPs = GameObject.FindGameObjectsWithTag(TagName);
        
        //randomly select one of these pieces...

        selectedPiece = AIPs[Random.Range(0, AIPs.Length)];
        //CurrentTile = selectedPiece.GetComponent<pieceMove>().MoveController.GetComponent<PawnMove>().currentTile;

        return selectedPiece;
    }

    public GameObject FindTile(GameObject AIPiece, string TagName)//if the enemy piece has no available moves it will break....looking for solution. possibly select different piece to move
    {
        GameObject selectedTile = null;

        //search for tiles around selectedAIPiece 
        //try using OverlapShpere()
        print("Array Initiated");
        Collider[] TilesWithinRange = Physics.OverlapSphere(AIPiece.transform.position, 3);
        int pieceIndex = 0;
        Debug.Log("ObjectWithinRange length: " + TilesWithinRange.Length);
        for (int i = 0; i < TilesWithinRange.Length; i++)
        {
            Debug.Log("[" + i + "] name: " + TilesWithinRange[i].name);
        }

            do
            {
                int tempIndex = Random.Range(1, TilesWithinRange.Length);
                GameObject tempTile = TilesWithinRange[tempIndex].gameObject;
                if (tempTile.tag == "WhiteTile")
                {
                    if(tempTile.GetComponent<TileProperties>().UnitOnTile == null)
                    {
                        pieceIndex = tempIndex;
                    }
                }
            } while (pieceIndex <= 0);
            //pieceIndex = Random.Range(0, TilesWithinRange.Length);
        selectedTile = TilesWithinRange[pieceIndex].gameObject;


        return selectedTile; 
    }

    public GameObject FindNearestPiece(GameObject AIPiece, string PlayerTag)//selects an opponents piece that is closest to the selected piece also depends on which Tag you send in(Black/White)
    {
        GameObject PlayerPiece = null;

        //collect all pieces with given tag...

        GameObject[] PlayerPs;
        PlayerPs = GameObject.FindGameObjectsWithTag(PlayerTag);

        //find the player piece that is closest to selected AIPiece...

        float distance = Mathf.Infinity;
        Vector3 disPosition = AIPiece.transform.position;
        foreach (GameObject Piece in PlayerPs)
        {
            Vector3 diff = Piece.transform.position - disPosition;
            float curDustance = diff.sqrMagnitude;
            if (curDustance < distance)
            {
                PlayerPiece = Piece;
                distance = curDustance;
            }
        }

        return PlayerPiece; 
    }

    public void SetTargetAI(GameObject SelectedPiece, GameObject TargetPiece)//only called if PlayerPiece within radius
    {
        //this sets the target for the AI Opponent and moves initiates combat...
        Vector3 targetPos = TargetPiece.transform.position;
        SelectedPiece.GetComponent<pieceMove>().targetPosition = targetPos;
        SelectedPiece.GetComponent<pieceMove>().GetNewPath();
        SelectedPiece.GetComponent<pieceMove>().isMoving = true;
        SelectedPiece.GetComponent<pieceMove>().datTile = TargetPiece;
        SelectedPiece.GetComponent<pieceMove>().datTile.GetComponent<TileProperties>().UnitOnTile = SelectedPiece;
        
        
        
    }
}
