using UnityEngine;
using System.Collections;

public class Player1MovementController : MonoBehaviour {

    public float speed = 6.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 lastLooking;

    void Start()
    {
        lastLooking = transform.forward;
    }
    void Update()
    {

        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {

            if (Input.GetAxis("360_HorizontalRightStick1") == 0 && Input.GetAxis("360_VerticalRightStick1") == 0)
            {
                transform.forward = lastLooking;
            }
            else
            {
                transform.forward = new Vector3(Input.GetAxis("360_HorizontalRightStick1"), 0, Input.GetAxis("360_VerticalRightStick1"));
            }
            //transform.forward = new Vector3(Input.GetAxis("360_HorizontalRightStick1"), 0, Input.GetAxis("360_VerticalRightStick1"));
            moveDirection = new Vector3(Input.GetAxis("360_HorizontalLeftStick1"), 0, Input.GetAxis("360_VerticalLeftStick1"));
            //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            
            //moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            
        }
        lastLooking = transform.forward;
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
