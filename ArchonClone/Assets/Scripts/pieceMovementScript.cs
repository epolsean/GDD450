using UnityEngine;
using System.Collections;
using Pathfinding;

public class pieceMovementScript : MonoBehaviour {

    public Vector3 targetPosition;
    public Seeker seeker;
    public CharacterController controller;
    public Path path;
    public float health = 10;
    public float speed = 300;
    public float nextWaypointDistance = 2f;
    int currentWaypoint = 0;
    public bool isMoving = false;
    public bool startedPath = false; 
    public GameObject MoveController;
    public Animator PieceAnim;
    public double MaxPieceMove;
    public float MaxMove;
    public int MaxPathNodes;
    public bool canMove2Tile = false;
    public bool canFight = false;
    public bool startMove = false; 
    public bool UIShouldGo = false;
    public GameObject datTile;
    public GameObject datSprite;
    public GameObject SoundController;

    void Start()
    {
        //plays animation if it is implemented/made yet
        MoveController = GameObject.Find("TurnController");
        SoundController = GameObject.Find("UISoundController");
        PieceAnim = GetComponentInChildren<Animator>();
        //set all piece variables that need to be set(e.g. MaxPathNodes)
        if (name == "WhiteTank(Clone)" || name == "BlackTank(Clone)")
        {
            MaxPieceMove = 5 * 2;
            MaxMove = 8;
            MaxPathNodes = 3;
        }
        else if (name == "WhiteGrunt(Clone)" || name == "BlackGrunt(Clone)")
        {
            MaxPieceMove = 5 * 2.75;
            MaxMove = 11;
            MaxPathNodes = 4;
        }
        else if (name == "WhiteRunner(Clone)" || name == "BlackRunner(Clone)")
        {
            MaxPieceMove = 5 * 3.5;
            MaxMove = 16;
            MaxPathNodes = 6;
        }
        else if (name == "WhiteScout(Clone)" || name == "BlackScout(Clone)")
        {
            MaxPieceMove = 5 * 2.75;
            MaxMove = 11;
            MaxPathNodes = 5;
        }
        else if (name == "WhiteBomber(Clone)" || name == "BlackBomber(Clone)")
        {
            MaxPieceMove = 5 * 2;
            MaxMove = 6;
            MaxPathNodes = 3;
        }
    }

    public void GetNewPath()
    {
        Debug.Log("Generating Path");
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);

        if (name == "BlackGrunt(Clone)" || name == "BlackTank(Clone)" || name == "WhiteTank(Clone)" || name == "BlackRunner(Clone)")
        {
            //PieceAnim.SetTrigger("WalkOnce");
            PieceAnim.SetBool("isWalking", true);
            if(name == "BlackRunner(Clone)")
            {
                print("Runner passed bool send------------------------------------------------------------------");
            }
        }
    }

    public int CalcNewPathDist()
    {
        Path p = seeker.GetNewPath(transform.position, targetPosition);
        print("PathNodeCompacity: " + p.vectorPath.Capacity);
        return p.vectorPath.Capacity;

        
    }

    public void GenNewPath()
    {
        seeker.GetNewPath(transform.position, targetPosition);
    }

    void OnPathComplete(Path newPath)
    {
        if (!newPath.error)
        {
            path = newPath;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }
        else
        {
            if(isMoving)
            {

            }
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            if(isMoving)
            {

                isMoving = false;
                if (name == "BlackGrunt(Clone)" || name == "BlackTank(Clone)" || name == "WhiteTank(Clone)" || name == "BlackRunner(Clone)")
                {
                    //GetComponentInChildren<Animator>().SetBool("isWalking", false);
                    PieceAnim.SetBool("isWalking", false);
                } 
                //Debug.Log("DA END");
                isMoving = false;
                startMove = false;
                MoveController.GetComponent<OnTurnActions>().EndOfTurn();
                MoveController.GetComponent<OnTurnActions>().ResetController();
                GridManager.rescan = true;
                //Debug.Log("Setting piece transform to target transform!");
                //transform.position = targetPosition;
                //print("Path Waypoint Count: " + path.vectorPath.Count);
                
                /*if (datTile.GetComponent<TileProperties>().fighting == false)
                {
                    if (TurnStateMachine.state == TurnStateMachine.State.playerTurn)
                    {
                        TurnStateMachine.state = TurnStateMachine.State.otherTurn;
                    }
                    else
                    {
                        TurnStateMachine.state = TurnStateMachine.State.playerTurn;
                        EnemyAI.AIstate = EnemyAI.State.Idle;
                    }
                }
                if(GameObject.Find("EnemyTurnController") != null)
                {
                    if (TurnStateMachine.state == TurnStateMachine.State.playerTurn)
                    {
                        TurnStateMachine.state = TurnStateMachine.State.otherTurn;
                    }
                    else
                    {
                        TurnStateMachine.state = TurnStateMachine.State.playerTurn;
                    }
                }*/
                transform.position = targetPosition;
                Debug.Log("Setting piece transform to target transform!");
                //MoveController.GetComponent<OnTurnActions>().NextTurn(); 
                
            }
            return;
        }
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        dir *= speed * Time.fixedDeltaTime;
        if(startMove)
        {
            if (path.vectorPath.Count <= MaxPathNodes)
            {
                controller.SimpleMove(dir);
                canMove2Tile = true;
                if (canFight)
                {
                    if (UIShouldGo)
                    {
                        UIShouldGo = false;
                        SoundController.GetComponent<UISoundsScript>().playFight();
                    }
                    MoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().fighting = true;
                }
                else
                {
                    if (UIShouldGo)
                    {
                        UIShouldGo = false;
                        SoundController.GetComponent<UISoundsScript>().playMovePiece();
                    }
                }
            }
            else
            {
                SoundController.GetComponent<UISoundsScript>().playError();
                path = null;
                //play error sound & reset turn piece is being assinged to the selected tile no matter what reset the tile that the unit is on 
                TurnStateMachine.canSelectPiece = true;
                TileProperties.pieceSelected = false;
                //MoveController.GetComponent<PawnMove>().isMoving = false;
                canFight = false;

            }
        }
        else//else checks for the path distance in nodes when you hover over a tile after selecting a piece
        {
            print("path Nodes: " + path.vectorPath.Count + " list: " + path.path.Count);
            if(MoveController.GetComponent<OnTurnActions>().drawnPath == false)
            {
                MoveController.GetComponent<OnTurnActions>().drawnPath = true; 
                Vector3[] tileNodes = path.vectorPath.ToArray();
                for (int i = 0; i < path.vectorPath.Count; i++)
                {
                    Collider[] NodeTile = Physics.OverlapSphere(new Vector3(tileNodes[i].x, tileNodes[i].y, tileNodes[i].z), 1);
                    print("NodeTile Size: " + NodeTile.Length + "at TileNodes: " + i);
                    for (int j = 0; j < NodeTile.Length; j++)
                    {
                        if (NodeTile[j].tag == "Tile")
                        {
                            NodeTile[j].renderer.material.color = Color.blue;
                        }
                    }
                }
            }
        }

        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance - 1.75)
        {
            currentWaypoint++;
        }
    }

    void Update()
    {
        if(isMoving && canMove2Tile)
        {
            var rotationAngle = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, Time.deltaTime*5);
            transform.rotation = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w);
        }
    }
}
