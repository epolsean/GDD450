using UnityEngine;
using System.Collections;
using Pathfinding;

public class AI : MonoBehaviour {

    public Vector3 targetPosition;
    public Seeker seeker;
    public CharacterController controller;
    public Path path;
    public float speed = 1000;
    public float nextWaypointDistance = 2f;
    int currentWaypoint = 0;

    void Start()
    {
        targetPosition = GameObject.FindWithTag("GroundTargetObject").transform.position;
        GetNewPath();
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
            return;
        }

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        dir *= speed * Time.fixedDeltaTime;

        controller.SimpleMove(dir);

        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
