using UnityEngine;
using System.Collections;

public class CameraZoomController : MonoBehaviour
{
    Quaternion startRotation;
    Vector3 startPosition;
    bool following = false;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update() 
    {
        if (!following)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            transform.Translate(Input.GetAxis("Mouse ScrollWheel") * ray.direction);

            if ((Input.mousePosition.x > Screen.width - 50) || Input.GetAxis("Horizontal") > 0)
            {
                MoveCameraRight();
            }

            if ((Input.mousePosition.x < 0 + 50)|| Input.GetAxis("Horizontal") < 0)
            {
                MoveCameraLeft();
            }

            if ((Input.mousePosition.y > Screen.height - 50)|| Input.GetAxis("Vertical") > 0)
            {
                MoveCameraUp();
            }

            if ((Input.mousePosition.y < 0 + 50) || Input.GetAxis("Vertical") < 0)
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
        transform.position = new Vector3(transform.position.x, transform.position.y, target.transform.position.z);
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
