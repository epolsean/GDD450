using UnityEngine;
using System.Collections;

public class Player2MovementController : MonoBehaviour
{

    public int xSensitivity = 5;
    public int ySensitivity = 5;

    public float speed = 6.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 lastLooking;
    public bool topDownView = true;

    void Start()
    {
        lastLooking = transform.forward;
    }
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (topDownView)
        {
            if (controller.isGrounded)
            {
                if (Input.GetAxis("360_HorizontalRightStick2") == 0 && Input.GetAxis("360_VerticalRightStick2") == 0)
                {
                    transform.forward = lastLooking;
                }
                else
                {
                    transform.forward = new Vector3(Input.GetAxis("360_HorizontalRightStick2"), 0, Input.GetAxis("360_VerticalRightStick2"));
                }
                //transform.forward = new Vector3(Input.GetAxis("360_HorizontalRightStick1"), 0, Input.GetAxis("360_VerticalRightStick1"));
                moveDirection = new Vector3(Input.GetAxis("360_HorizontalLeftStick2"), 0, Input.GetAxis("360_VerticalLeftStick2"));
                //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                //moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;

            }
            lastLooking = transform.forward;
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
        else
        {
            if (controller.isGrounded)
            {
                transform.Rotate(Vector3.up, xSensitivity * Input.GetAxis("360_HorizontalRightStick2"));

                moveDirection = new Vector3(Input.GetAxis("360_HorizontalLeftStick2"), 0, Input.GetAxis("360_VerticalLeftStick2"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
}
