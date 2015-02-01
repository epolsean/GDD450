using UnityEngine;
using System.Collections;

public class CameraZoomController : MonoBehaviour
{
    Quaternion startRotation;
    Vector3 startPosition;
    bool following = false;

    public int minZoomLevel = 10; //How far the camera can zoom out
    public int maxZoomLevel = 10; //How far the camera can zoom in

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update() 
    {
        Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        if (!following)
        {
            if (transform.position.y < (startPosition.y + minZoomLevel) && Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                transform.Translate(Input.GetAxis("Mouse ScrollWheel") * 10 * transform.forward, Space.World);
            }
            else if (transform.position.y > (startPosition.y - maxZoomLevel) && Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                transform.Translate(Input.GetAxis("Mouse ScrollWheel") * 10 * transform.forward, Space.World);
            }

            if ((((Input.mousePosition.x > Screen.width - 50) && (Input.mousePosition.x < Screen.width)) || Input.GetAxis("Horizontal") > 0) && transform.position.x < startPosition.x + 20)
            {
                MoveCameraRight();
            }

            if ((((Input.mousePosition.x < 0 + 50) && (Input.mousePosition.x > 0)) || Input.GetAxis("Horizontal") < 0) && transform.position.x > startPosition.x - 20)
            {
                MoveCameraLeft();
            }

            if ((((Input.mousePosition.y > Screen.height - 50) && (Input.mousePosition.y < Screen.height)) || Input.GetAxis("Vertical") > 0) && transform.position.z < startPosition.z + 20)
            {
                MoveCameraUp();
            }

            if ((((Input.mousePosition.y < 0 + 50) && (Input.mousePosition.y > 0)) || Input.GetAxis("Vertical") < 0) && transform.position.z > startPosition.z - 20)
            {
                MoveCameraDown();
            }
        }
	}

    // Method to set the target gameobject for the camera to zoom in on and follow
    public void FollowTarget(GameObject target)
    {
        following = true;
        transform.position = startPosition;
        transform.rotation = startRotation;
        //transform.position = new Vector3(transform.position.x, transform.position.y, target.transform.position.z);
        transform.forward = Vector3.Normalize(target.transform.position - transform.position);
        transform.Translate((Vector3.Distance(transform.position, target.transform.position) - 15) * transform.forward, Space.World);
        transform.SetParent(target.transform);
    }

    // Method to reset the camera transform after movement/interaction is done
    public void ResetTransform()
    {
        following = false;
        transform.SetParent(null);
        transform.position = startPosition;
        transform.rotation = startRotation;
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
