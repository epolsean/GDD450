using UnityEngine;
using System.Collections;

public class CameraZoomController : MonoBehaviour
{
    Quaternion startRotation;
    Vector3 startPosition;
    bool following = false;
    bool zooming = false;
    bool resetting = false;

    public int minZoomLevel = 10; //How far the camera can zoom out
    public int maxZoomLevel = 10; //How far the camera can zoom in

    GameObject targetTile;
    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update() 
    {
        if (!following && !zooming)
        {
            if (transform.position.y < (startPosition.y + minZoomLevel) && Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                transform.Translate(Input.GetAxis("Mouse ScrollWheel") * 10 * transform.forward, Space.World);
            }
            else if (transform.position.y > (startPosition.y - maxZoomLevel) && Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                transform.Translate(Input.GetAxis("Mouse ScrollWheel") * 10 * transform.forward, Space.World);
            }

            if ((((Input.mousePosition.x > Screen.width - Screen.width / 18) && (Input.mousePosition.x < Screen.width - Screen.width / 75) && (Input.mousePosition.y > 0) && (Input.mousePosition.y < Screen.height)) || Input.GetAxis("Horizontal") > 0) && transform.position.x < startPosition.x + 20)
            {
                MoveCameraRight();
            }

            if ((((Input.mousePosition.x < 0 + Screen.width / 18) && (Input.mousePosition.x > Screen.width / 75) && (Input.mousePosition.y > 0) && (Input.mousePosition.y < Screen.height)) || Input.GetAxis("Horizontal") < 0) && transform.position.x > startPosition.x - 20)
            {
                MoveCameraLeft();
            }

            if ((((Input.mousePosition.y > Screen.height - Screen.width / 18) && (Input.mousePosition.y < Screen.height - Screen.width / 75) && (Input.mousePosition.x > 0) && (Input.mousePosition.x < Screen.width)) || Input.GetAxis("Vertical") > 0) && transform.position.z < startPosition.z + 20)
            {
                MoveCameraUp();
            }

            if ((((Input.mousePosition.y < 0 + Screen.width / 18) && (Input.mousePosition.y > Screen.width / 75) && (Input.mousePosition.x > 0) && (Input.mousePosition.x < Screen.width)) || Input.GetAxis("Vertical") < 0) && transform.position.z > startPosition.z - 20)
            {
                MoveCameraDown();
            }
        }
        if (zooming && !following)
        {
            ZoomToTarget();
        }
        else if (resetting)
        {
            ResettingCamera();
        }
	}

    //Zooms in on target before the target starts to move
    void ZoomToTarget()
    {
        if (Vector3.Distance(transform.position, targetTile.transform.position) < 10)
        {
            following = true;
            zooming = false;
            targetTile.GetComponent<pieceMovementScript>().isMoving = true;
            targetTile.GetComponent<pieceMovementScript>().startMove = true;
        }
        else
        {
            transform.forward = Vector3.Lerp(transform.forward,Vector3.Normalize(targetTile.transform.position - transform.position),Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, targetTile.transform.position, Time.deltaTime);
        }
    }

    //Sets the target for the camera to follow
    public void SetTarget(GameObject target)
    {
        targetTile = target;
        zooming = true;
        transform.position = startPosition;
        transform.rotation = startRotation;
        //transform.forward = Vector3.Normalize(target.transform.position - transform.position);
        transform.SetParent(target.transform);
    }

    // Method to reset the camera transform after movement/interaction is done
    public void ResetTransform()
    {
        following = false;
        zooming = false;
        transform.SetParent(null);
        resetting = true;
    }

    void ResettingCamera()
    {
        transform.position = Vector3.Lerp(transform.position, startPosition,Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, startRotation, Time.deltaTime);

        if (Vector3.Distance(transform.position, startPosition) < 1)
        {
            resetting = false;
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
    }

    public void MoveCameraLeft()
    {
        transform.Translate(Vector3.right * 10 * -Time.deltaTime, Space.World);
    }

    public void MoveCameraRight()
    {
        transform.Translate(Vector3.right * 10 * Time.deltaTime, Space.World);
    }

    public void MoveCameraUp()
    {
        transform.Translate(Vector3.forward * 10 * Time.deltaTime, Space.World);
    }

    public void MoveCameraDown()
    {
        transform.Translate(Vector3.forward * 10 * -Time.deltaTime, Space.World);
    }
}
