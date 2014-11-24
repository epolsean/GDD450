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
    public int bulletSpeed = 25;

    public GameObject healthPieceGreen1;
    public GameObject healthPieceGreen2;
    public Sprite healthPieceRed;

    public bool swinging = false;
    float swingTimer = 0.3f;

    float health;
    float MaxHealth;
    int bulletSize;

    public bool win = false;
    bool reloading = false;
    float reloadTime = 0.8f;

    Behaviour halo;

    bool startTrans;
    float endTimer;
    GameObject Canvas;
    CharacterController controller;

    void Start()
    {
        Canvas = GameObject.Find("Canvas2");
        startTrans = false;
        endTimer = 0;
        halo = (Behaviour)GetComponent("Halo");
        health = 100;
        MaxHealth = 100;
        bulletSize = 1;
        lastLooking = transform.forward;
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (TutorialTextHints.curTutorialStep != 4)
        {
            if (win == false)
            {

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
                            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) + transform.right * Input.GetAxis("Strafe1");
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
                            moveDirection = transform.TransformDirection(moveDirection) + (transform.right * Input.GetAxis("Strafe1"));
                            moveDirection *= speed;
                        }
                    }
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
                            bulletSpeed = 25;
                            halo.enabled = false;
                            reloading = true;
                        }
                    }
                    else
                    {
                        if ((Input.GetAxis("Fire1") == 1) && reloading == false)
                        {
                            Rigidbody bulletClone = Instantiate(Bullet, transform.position + 1.2f * bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
                            bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                            bulletClone.rigidbody.useGravity = false;
                            bulletClone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
                            Destroy(bulletClone.gameObject, 3);
                            audio.Play();
                            bulletSize = 1;
                            bulletSpeed = 25;
                            halo.enabled = false;
                            reloading = true;
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
                    if (Input.GetAxis("360_RightTrigger1") == 1 && swinging == false)
                    {
                        GameObject sword = Instantiate(Sword, transform.position + this.transform.forward, transform.rotation) as GameObject;
                        sword.tag = tag;
                        swinging = true;
                        Destroy(sword.gameObject, 0.3f);
                    }
                }
            }

            healthPieceGreen1.GetComponent<Image>().fillAmount = (float)((health * 2) / (MaxHealth * 3));
            healthPieceGreen2.GetComponent<Image>().fillAmount = (float)((health * 2) / (MaxHealth * 3));
            if ((float)((health * 2) / (MaxHealth * 3)) <= 0.16f)
            {
                healthPieceGreen1.GetComponent<Image>().sprite = healthPieceRed;
                healthPieceGreen2.GetComponent<Image>().sprite = healthPieceRed;
            }
            if (win == true)
            {
                BattleStats.winner = tag;
                if (endTimer <= 2.5f)
                {
                    endTimer += Time.deltaTime;
                    if (endTimer >= 1.5f && endTimer < 1.6f)
                    {
                        Canvas.GetComponent<SceneTrans>().close = true;
                        Canvas.GetComponent<SceneTrans>().startTimer = 0;
                    }
                }
                else
                {
                    GameObject.Find("HexGrid").GetComponent<TileProperties>().cameBack = true;
                    Invoke("HasWon", 0f);
                    enabled = false;
                }
            }
        }
    }

    void HasWon()
    {
        //Application.LoadLevel("TutorialTestGrid");
        GameObject TutorialEnemyController = GameObject.Find("EnemyTurnController");
        TutorialEnemyController.GetComponent<TutorialEnemyBoardScript>().enemyTurn = 5.5;
        Destroy(GameObject.Find("TutorialTestBattle"));
    }
}
