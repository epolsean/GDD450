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

    void Start()
    {
        //targetPosition = GameObject.FindWithTag("GroundTargetObject").transform.position;
        //GetNewPath();
        MoveController = GameObject.Find("MovementController");
    }

    public void GetNewPath()
    {
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
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
                Debug.Log("DA END");
                GridManager.rescan = true;
                MoveController.GetComponent<PawnMove>().SelectedPiece.transform.position = targetPosition;//MoveController.GetComponent<PawnMove>().MoveToTile.transform.position;
            }
            isMoving = false;
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
