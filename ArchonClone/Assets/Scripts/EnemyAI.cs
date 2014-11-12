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
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator IdleState()
    {
        Debug.Log("EnemyIdle: Enter");
        while (AIstate == State.Idle)
        {
            print("Player's Turn");
            if(TurnStateMachine.state == TurnStateMachine.State.otherTurn)
            {
                AIstate = State.SelectingPiece;
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
        Debug.Log("Enemy" + AIstate.ToString() + ": Exit");
        while (AIstate == State.Idle)
        {
            AIstate = State.DaMove;
            AISelectedPiece = SelectRandP("Black");//this will give you a random black piece
            AITargetPlayerP = FindNearestPiece(AISelectedPiece, "White");
            if(Vector3.Distance(AISelectedPiece.transform.position, AITargetPlayerP.transform.position) <= AISelectedPiece.GetComponent<PawnMove>().MaxMove)
            {
                print("Closest PlayerPiece is too far from selectedPiece");
                AIFinalTurnTarget = AITargetPlayerP;
            }
            else
            {
                print("Finding random tile to move to");
                //set final target to be a random tile
            }
            yield return 0;
        }
        Debug.Log("Enemy" + AIstate.ToString() + ": Exit");
        if (Network.isClient || Network.isServer)
            networkView.RPC("NextState", RPCMode.AllBuffered);
        else
            NextState();
    }

    IEnumerator DaMoveState()//this state will determine if the enemy will attack a player or just select a random tile to move to
    {
        Debug.Log("Enemy" + AIstate.ToString() + ": Exit");
        while (AIstate == State.Idle)
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
            AIstate = State.Idle;
            TurnStateMachine.state = TurnStateMachine.State.playerTurn;
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

        return selectedPiece;
    }

    public GameObject FindTile(GameObject AIPiece, string TagName)
    {
        GameObject selectedTile = null;

        //search for tiles around selectedAIPiece 
        //try using OverlapShpere()
        Collider[] TilesWithinRange = Physics.OverlapSphere(AIPiece.transform.position, 2);


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
    }
}
