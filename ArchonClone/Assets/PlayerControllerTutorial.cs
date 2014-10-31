using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControllerTutorial : MonoBehaviour
{
    public static int xSensitivity = 3;
    public static int ySensitivity = 3;

    public enum Character { Grunt, Tank, Runner, Scout };
    public static Character myCharacter;

    public float speed = 6.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 lastLooking;
    public bool topDownView = false;

    public Rigidbody Bullet;
    public GameObject Sword;
    public bool isMelee = false;
    public int bulletSpeed = 20;

    public GameObject healthPiece1;
    public GameObject healthPiece2;

    public GameObject healthPiece3;
    public GameObject healthPiece4;

    public bool swinging = false;
    float swingTimer = 0.3f;

    int health;
    int bulletSize;
    float chargeTime = 2.0f;

    public bool win = false;
    bool reloading = false;
    float reloadTime = 0.8f;

    Behaviour halo;

    void Start()
    {
        //Determine character and set up stats
        if (myCharacter == Character.Grunt)
        {

        }
        else if (myCharacter == Character.Runner)
        {

        }
        else if (myCharacter == Character.Tank)
        {

        }
        else if (myCharacter == Character.Scout)
        {

        }
        halo = (Behaviour)GetComponent("Halo");
        health = 100;
        bulletSize = 1;
        lastLooking = transform.forward;
    }
    void Update()
    {
        if (TutorialTextHints.curTutorialStep != 4)
        {
            CharacterController controller = GetComponent<CharacterController>();
            if (topDownView)
            {
                if (controller.isGrounded)
                {
                    if (Input.GetJoystickNames().Length != 0)
                    {
                        if (Input.GetAxis("360_HorizontalRightStick1") == 0 && Input.GetAxis("360_VerticalRightStick1") == 0)
                        {
                            transform.forward = lastLooking;
                        }
                        else
                        {
                            transform.forward = new Vector3(Input.GetAxis("360_HorizontalRightStick1"), 0, Input.GetAxis("360_VerticalRightStick1"));
                        }
                        moveDirection = new Vector3(Input.GetAxis("360_HorizontalLeftStick1"), 0, Input.GetAxis("360_VerticalLeftStick1"));
                        moveDirection *= speed;
                    }
                    else
                    {
                        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
                        {
                            transform.forward = lastLooking;
                        }
                        else
                        {
                            transform.forward = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                        }
                        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                        moveDirection *= speed;
                    }

                }
                lastLooking = transform.forward;
                moveDirection.y -= gravity * Time.deltaTime;
                controller.Move(moveDirection * Time.deltaTime);
            }
            else
            {
                if (controller.isGrounded)
                {
                    if (Input.GetJoystickNames().Length != 0)
                    {
                        transform.Rotate(Vector3.up, xSensitivity * Input.GetAxis("360_HorizontalRightStick1"));

                        moveDirection = new Vector3(Input.GetAxis("360_HorizontalLeftStick1"), 0, Input.GetAxis("360_VerticalLeftStick1"));
                        moveDirection = transform.TransformDirection(moveDirection);
                        moveDirection *= speed;
                    }
                    else
                    {
                        transform.Rotate(transform.up, xSensitivity * Input.GetAxis("Horizontal"));
                        if (Input.GetAxis("Vertical") == 0)
                        {
                            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
                        }
                        else
                        {
                            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                        }
                        moveDirection = transform.TransformDirection(moveDirection);
                        moveDirection *= speed;
                    }
                }
                Debug.Log("Horizontal " + Input.GetAxis("Horizontal"));
                moveDirection.y -= gravity * Time.deltaTime;
                controller.Move(moveDirection * Time.deltaTime);
            }
            if (isMelee == false)
            {
                if (reloading)
                {
                    reloadTime -= Time.deltaTime;
                    if (reloadTime < 0)
                    {
                        reloadTime = 0.8f;
                        reloading = false;
                    }
                }

                /*if ((Input.GetAxis("Fire1") == 1) && bulletSize == 1 && reloading == false)
                {
                    chargeTime -= Time.deltaTime;
                    if (chargeTime < 0)
                    {
                        halo.enabled = true;
                        bulletSize = 2;
                        bulletSpeed -= 4;
                    }
                }*/

                if (win == false)
                {
                    if (Input.GetJoystickNames().Length != 0)
                    {
                        if ((Input.GetAxis("360_RightTrigger1") == 1) && reloading == false)
                        {
                            Rigidbody bulletClone = Instantiate(Bullet, transform.position + 1.2f * bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
                            bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                            bulletClone.rigidbody.useGravity = false;
                            bulletClone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
                            Destroy(bulletClone.gameObject, 3);
                            audio.Play();
                            bulletSize = 1;
                            bulletSpeed = 20;
                            chargeTime = 0.5f;
                            halo.enabled = false;
                            reloading = true;
                        }
                    }
                    else
                    {
                        if ((Input.GetButtonUp("Fire1")) && reloading == false)
                        {
                            Rigidbody bulletClone = Instantiate(Bullet, transform.position + 1.2f * bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
                            bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                            bulletClone.rigidbody.useGravity = false;
                            bulletClone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
                            Destroy(bulletClone.gameObject, 3);
                            audio.Play();
                            bulletSize = 1;
                            bulletSpeed = 20;
                            chargeTime = 0.5f;
                            halo.enabled = false;
                            reloading = true;
                        }
                    }
                }
            }
            else
            {
                if (swinging)
                {
                    swingTimer -= Time.deltaTime;
                    if (swingTimer < 0)
                    {
                        swingTimer = 0.3f;
                        swinging = false;
                    }
                }

                if (win == false)
                {
                    if (Input.GetAxis("360_RightTrigger1") == 1 && swinging == false)
                    {
                        GameObject sword = Instantiate(Sword, transform.position + this.transform.forward, transform.rotation) as GameObject;
                        sword.tag = tag;
                        swinging = true;
                        Destroy(sword.gameObject, 0.3f);
                    }
                }
            }

            healthPiece1.GetComponent<Image>().fillAmount = (float)((float)health / 200);
            healthPiece2.GetComponent<Image>().fillAmount = (float)((float)health / 200);
            healthPiece3.GetComponent<Image>().fillAmount = (float)((float)health / 200);
            healthPiece4.GetComponent<Image>().fillAmount = (float)((float)health / 200);

            if (health <= 30)
            {
                healthPiece1.GetComponent<Image>().color = Color.red;
                healthPiece2.GetComponent<Image>().color = Color.red;
                healthPiece3.GetComponent<Image>().color = Color.red;
                healthPiece4.GetComponent<Image>().color = Color.red;
            }
            if (win == true)
            {
                BattleStats.winner = tag;
                //Destroy(GameObject.Find("P1 Health Text"));
                //Destroy(GameObject.Find("P2 Health Text"));
                //Application.LoadLevel("TutorialTestGrid");
                //Destroy(this.gameObject);
                Invoke("HasWon", 2.4f);
                enabled = false;
            }
        }
    }

    void HasWon()
    {
        //Application.LoadLevel("TutorialTestGrid");
        GameObject TutorialEnemyController = GameObject.Find("EnemyTurnController");
        TutorialEnemyController.GetComponent<TutorialEnemyBoardScript>().enemyTurn = 4.5;
        Destroy(GameObject.Find("TutorialTestBattle"));
    }
}
