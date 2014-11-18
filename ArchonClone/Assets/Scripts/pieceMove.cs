using UnityEngine;
using System.Collections;
using Pathfinding;

public class pieceMove : MonoBehaviour {

    public Vector3 targetPosition;
    public Seeker seeker;
    public CharacterController controller;
    public Path path;
    public float health = 10;
    public float speed = 300;
    public float nextWaypointDistance = 2f;
    int currentWaypoint = 0;
    public bool isMoving = false;
    public GameObject MoveController;
    public Animator PieceAnim;
    public double MaxPieceMove;
    public GameObject datTile; 

    void Start()
    {
        //targetPosition = GameObject.FindWithTag("GroundTargetObject").transform.position;
        //GetNewPath();
        MoveController = GameObject.Find("MovementController");
        if(name == "BlackGrunt(Clone)" || name == "BlackTank(Clone)" || name == "WhiteTank(Clone)")
        {
            print("Set Piece Animator");
            PieceAnim = GetComponentInChildren<Animator>();
            print("Animator Set = true");
        }
        if (name == "WhiteTank(Clone)" || name == "BlackTank(Clone)")
        {
            MaxPieceMove = 5 * 2;
        }
        else if (name == "WhiteGrunt(Clone)" || name == "BlackGrunt(Clone)")
        {
            MaxPieceMove = 5 * 2.75;
        }
        else if (name == "WhiteRunner(Clone)" || name == "BlackRunner(Clone)")
        {
            MaxPieceMove = 5 * 3.5;
        }
        else if (name == "WhiteScout(Clone)" || name == "BlackScout(Clone)")
        {
            MaxPieceMove = 5 * 2.75;
        }
               
    }

    public void GetNewPath()
    {
        //Path p = seeker.GetNewPath(transform.position, targetPosition);
        //if (p.vectorPath.Count > 0)
        //{
            seeker.StartPath(transform.position, targetPosition, OnPathComplete);
            if (name == "BlackGrunt(Clone)" || name == "BlackTank(Clone)" || name == "WhiteTank(Clone)")
            {
                //PieceAnim.SetTrigger("WalkOnce");
                PieceAnim.SetBool("isWalking", true);
            }
        //}
    }

    public int CalcNewPathDist()
    {
        Path p = seeker.GetNewPath(transform.position, targetPosition);
        print("PathNodeCopacity: " + p.vectorPath.Capacity);
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
        if (currentWaypoint >= path.vectorPath.Count)
        {
            if(isMoving)
            {

                if (name == "BlackGrunt(Clone)" || name == "BlackTank(Clone)" || name == "WhiteTank(Clone)")
                {
                    //GetComponentInChildren<Animator>().SetBool("isWalking", false);
                    PieceAnim.SetBool("isWalking", false);
                } 
                Debug.Log("DA END");
                GridManager.rescan = true;
                transform.position = targetPosition;
                
                isMoving = false;
                print("Path Waypoint Count: " + path.vectorPath.Count);
                if (datTile.GetComponent<TileProperties>().fighting == false)
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
                
            }
            
            //MoveController.GetComponent<PawnMove>().currentTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
            //MoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(false);
            
            return;
        }

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        dir *= speed * Time.fixedDeltaTime;

        controller.SimpleMove(dir);

        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance - 1)
        {
            currentWaypoint++;
        }
        else if(path.vectorPath[currentWaypoint] == targetPosition)
        {
            Debug.Log("I'm Here");
        }
    }

    void Update()
    {
        if(isMoving)
        {
            var rotationAngle = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, Time.deltaTime*5);
            transform.rotation = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w);
        }
    }
}
