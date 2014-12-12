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
    public bool startedPath = false; 
    public GameObject MoveController;
    public Animator PieceAnim;
    public double MaxPieceMove;
    public float MaxMove;
    public int MaxPathNodes;
    public bool canMove2Tile = false;
    public bool canFight = false; 
    public GameObject datTile;
    public GameObject datSprite; 

    void Start()
    {
        //plays animation if it is implemented/made yet
        MoveController = GameObject.Find("MovementController");
        if(name == "BlackGrunt(Clone)" || name == "BlackTank(Clone)" || name == "WhiteTank(Clone)")
        {
            print("Set Piece Animator");
            PieceAnim = GetComponentInChildren<Animator>();
            print("Animator Set = true");
        }
        //set pieces maxMove variable
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
            MaxPathNodes = 5;
        }
        else if (name == "WhiteScout(Clone)" || name == "BlackScout(Clone)")
        {
            MaxPieceMove = 5 * 2.75;
            MaxMove = 11;
            MaxPathNodes = 4;
        }

        //assign pieces to their sprites
        if(datTile.name == "HexGrid(WGrunt01Tile)")
        {
            if (GameObject.Find("SynthGrunt01Sprite") != null)
            {
                datSprite = GameObject.Find("SynthGrunt01Sprite");
            }
        }
        else if (datTile.name == "HexGrid(WGrunt02Tile)")
        {
            if (GameObject.Find("SynthGrunt02Sprite") != null)
            {
                datSprite = GameObject.Find("SynthGrunt02Sprite");
            }
        }
        else if (datTile.name == "HexGrid(WGrunt03Tile)")
        {
            if (GameObject.Find("SynthGrunt03Sprite") != null)
            {
                datSprite = GameObject.Find("SynthGrunt03Sprite");
            }
        }
        else if (datTile.name == "HexGrid(WGrunt04Tile)")
        {
            if (GameObject.Find("SynthGrunt04Sprite") != null)
            {
                datSprite = GameObject.Find("SynthGrunt04Sprite");
            }
        }
        else if (datTile.name == "HexGrid(WTank01Tile)")
        {
            if (GameObject.Find("SynthTank01Sprite") != null)
            {
                datSprite = GameObject.Find("SynthTank01Sprite");
            }
        }
        else if (datTile.name == "HexGrid(WTank02Tile)")
        {
            if (GameObject.Find("SynthTank02Sprite") != null)
            {
                datSprite = GameObject.Find("SynthTank02Sprite");
            }
        }
        else if (datTile.name == "HexGrid(WTank03Tile)")
        {
            if (GameObject.Find("SynthTank03Sprite") != null)
            {
                datSprite = GameObject.Find("SynthTank03Sprite");
            }
        }
        else if (datTile.name == "HexGrid(WTank04Tile)")
        {
            if (GameObject.Find("SynthTank04Sprite") != null)
            {
                datSprite = GameObject.Find("SynthTank04Sprite");
            }
        }
        else if (datTile.name == "HexGrid(WScout01Tile)")
        {
            if (GameObject.Find("SynthScout01Sprite") != null)
            {
                datSprite = GameObject.Find("SynthScout01Sprite");
            }
        }
        else if (datTile.name == "HexGrid(WScout02Tile)")
        {
            if (GameObject.Find("SynthScout02Sprite") != null)
            {
                datSprite = GameObject.Find("SynthScout02Sprite");
            }
        }
        else if (datTile.name == "HexGrid(WScout03Tile)")
        {
            if (GameObject.Find("SynthScout03Sprite") != null)
            {
                datSprite = GameObject.Find("SynthScout03Sprite");
            }
        }
        else if (datTile.name == "HexGrid(WScout04Tile)")
        {
            if (GameObject.Find("SynthScout04Sprite") != null)
            {
                datSprite = GameObject.Find("SynthScout04Sprite");
            }
        }
        else if (datTile.name == "HexGrid(WRunner01Tile)")
        {
            if (GameObject.Find("SynthRunner01Sprite") != null)
            {
                datSprite = GameObject.Find("SynthRunner01Sprite");
            }
        }
        else if (datTile.name == "HexGrid(WRunner02Tile)")
        {
            if (GameObject.Find("SynthRunner02Sprite") != null)
            {
                datSprite = GameObject.Find("SynthRunner02Sprite");
            }
        }
        else if (datTile.name == "HexGrid(WRunner03Tile)")
        {
            if (GameObject.Find("SynthRunner03Sprite") != null)
            {
                datSprite = GameObject.Find("SynthRunner03Sprite");
            }
        }
        else if (datTile.name == "HexGrid(WRunner04Tile)")
        {
            if (GameObject.Find("SynthRunner04Sprite") != null)
            {
                datSprite = GameObject.Find("SynthRunner04Sprite");
            }
        }
        else if (datTile.name == "HexGrid(BGrunt01Tile)")//assign all Alien piece sprites
        {
            if (GameObject.Find("AlienGrunt01Sprite") != null)
            {
                datSprite = GameObject.Find("AlienGrunt01Sprite");
            }
        }
        else if (datTile.name == "HexGrid(BGrunt02Tile)")
        {
            if (GameObject.Find("AlienGrunt02Sprite") != null)
            {
                datSprite = GameObject.Find("AlienGrunt02Sprite");
            }
        }
        else if (datTile.name == "HexGrid(BGrunt03Tile)")
        {
            if (GameObject.Find("AlienGrunt03Sprite") != null)
            {
                datSprite = GameObject.Find("AlienGrunt03Sprite");
            }
        }
        else if (datTile.name == "HexGrid(BGrunt04Tile)")
        {
            if (GameObject.Find("AlienGrunt04Sprite") != null)
            {
                datSprite = GameObject.Find("AlienGrunt04Sprite");
            }
        }
        else if (datTile.name == "HexGrid(BTank01Tile)")
        {
            if (GameObject.Find("AlienTank01Sprite") != null)
            {
                datSprite = GameObject.Find("AlienTank01Sprite");
            }
        }
        else if (datTile.name == "HexGrid(BTank02Tile)")
        {
            if (GameObject.Find("AlienTank02Sprite") != null)
            {
                datSprite = GameObject.Find("AlienTank02Sprite");
            }
        }
        else if (datTile.name == "HexGrid(BTank03Tile)")
        {
            if (GameObject.Find("AlienTank03Sprite") != null)
            {
                datSprite = GameObject.Find("AlienTank03Sprite");
            }
        }
        else if (datTile.name == "HexGrid(BTank04Tile)")
        {
            if (GameObject.Find("AlienTank04Sprite") != null)
            {
                datSprite = GameObject.Find("AlienTank04Sprite");
            }
        }
        else if (datTile.name == "HexGrid(BScout01Tile)")
        {
            if (GameObject.Find("AlienScout01Sprite") != null)
            {
                datSprite = GameObject.Find("AlienScout01Sprite");
            }
        }
        else if (datTile.name == "HexGrid(BScout02Tile)")
        {
            if (GameObject.Find("AlienScout02Sprite") != null)
            {
                datSprite = GameObject.Find("AlienScout02Sprite");
            }
        }
        else if (datTile.name == "HexGrid(BScout03Tile)")
        {
            if (GameObject.Find("AlienScout03Sprite") != null)
            {
                datSprite = GameObject.Find("AlienScout03Sprite");
            }
        }
        else if (datTile.name == "HexGrid(BScout04Tile)")
        {
            if (GameObject.Find("AlienScout04Sprite") != null)
            {
                datSprite = GameObject.Find("AlienScout04Sprite");
            }
        }
        else if (datTile.name == "HexGrid(BRunner01Tile)")
        {
            if (GameObject.Find("AlienRunner01Sprite") != null)
            {
                datSprite = GameObject.Find("AlienRunner01Sprite");
            }
        }
        else if (datTile.name == "HexGrid(BRunner02Tile)")
        {
            if (GameObject.Find("AlienRunner02Sprite") != null)
            {
                datSprite = GameObject.Find("AlienRunner02Sprite");
            }
        }
        else if (datTile.name == "HexGrid(BRunner03Tile)")
        {
            if (GameObject.Find("AlienRunner03Sprite") != null)
            {
                datSprite = GameObject.Find("AlienRunner03Sprite");
            }
        }
        else if (datTile.name == "HexGrid(BRunner04Tile)")
        {
            if (GameObject.Find("AlienRunner04Sprite") != null)
            {
                datSprite = GameObject.Find("AlienRunner04Sprite");
            }
        }

        //turn on sprite if assigned one
        if(datSprite != null)
        {
            datSprite.SetActive(true);
        }
               
    }

    public void GetNewPath()
    {
        Debug.Log("Generating Path");
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
        
        if (name == "BlackGrunt(Clone)" || name == "BlackTank(Clone)" || name == "WhiteTank(Clone)")
        {
            //PieceAnim.SetTrigger("WalkOnce");
            PieceAnim.SetBool("isWalking", true);
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
                if (name == "BlackGrunt(Clone)" || name == "BlackTank(Clone)" || name == "WhiteTank(Clone)")
                {
                    //GetComponentInChildren<Animator>().SetBool("isWalking", false);
                    PieceAnim.SetBool("isWalking", false);
                } 
                //Debug.Log("DA END");
                canMove2Tile = false;
                MoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().UnitOnTile = this.gameObject;
                print("b4 old tile reset");
                MoveController.GetComponent<PawnMove>().currentTile.GetComponent<TileProperties>().UnitOnTile = null;
                print("after old tile reset");
                MoveController.GetComponent<PawnMove>().currentTile.GetComponent<TileProperties>().datNode.SetActive(true);
                //MoveController.GetComponent<PawnMove>().MoveToTile.renderer.material.color = Color.magenta;
                //MoveController.GetComponent<PawnMove>().currentTile.renderer.material.color = Color.blue;
                
                this.datTile = MoveController.GetComponent<PawnMove>().MoveToTile;
                MoveController.GetComponent<PawnMove>().isMoving = false;
                datTile.GetComponent<TileProperties>().canPlace = false;
                //MoveController.GetComponent<PawnMove>().MoveToTile = null;
                GridManager.rescan = true;
                //Debug.Log("Setting piece transform to target transform!");
                //transform.position = targetPosition;
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
                transform.position = targetPosition;
                Debug.Log("Setting piece transform to target transform!");
                
            }
            
            //MoveController.GetComponent<PawnMove>().currentTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
            //MoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(false);
            
            return;
        }
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        dir *= speed * Time.fixedDeltaTime;
        if(path.vectorPath.Count <= MaxPathNodes)
        {
            controller.SimpleMove(dir);
            canMove2Tile = true; 
            if(canFight)
            {
                MoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().fighting = true; 
            }
        }
        else
        {
            path = null;
            //play error sound & reset turn piece is being assinged to the selected tile no matter what reset the tile that the unit is on 
            TurnStateMachine.canSelectPiece = true;
            TileProperties.pieceSelected = false;
            MoveController.GetComponent<PawnMove>().isMoving = false;
            canFight = false; 

        }

        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance - 1)
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
