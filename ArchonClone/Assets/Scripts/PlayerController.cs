using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject SynthTank;
    public GameObject OrganicGrunt;

    public static int xSensitivity = 3;
    public static int ySensitivity = 3;

    public float speed = 6.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 lastLooking;
    public bool topDownView = false;

    public Rigidbody Bullet;
    public GameObject Sword;
    public bool isMelee = false;
    public int bulletSpeed = 25;

    public GameObject healthPieceGreen;
    public Sprite healthPieceRed;

    public Camera camMine;
    public Camera camEnemy;

    public bool swinging = false;
    float swingTimer = 0.5f;

    Player1MovementController self1;
    Player2MovementController self2;
    Player2MovementController enemy1;
    Player1MovementController enemy2;

    public float health;
    public float MaxHealth;
    int bulletSize;

    public bool win = false;
    bool reloading = false;
    float reloadTime = 0.8f;
    CharacterController controller;

    //public GameObject MoveController;

    void Awake()
    {
        //LAN Switch
        if (!Network.isClient && !Network.isServer)
            this.enabled = false;
        else if (Network.isClient || Network.isServer)
        {
            if (this.GetComponent<Player1MovementController>())
            {
                self1 = this.GetComponent<Player1MovementController>();
                self1.enabled = false;
                SynthTank.SetActive(true);
                enemy1 = GameObject.Find("Player2(Clone)").GetComponent<Player2MovementController>();
            }
            else
            {
                self2 = this.GetComponent<Player2MovementController>();
                self2.enabled = false;
                OrganicGrunt.SetActive(true);
                enemy2 = GameObject.Find("Player1(Clone)").GetComponent<Player1MovementController>();
            }
        }
    }

    void Start()
    {
        //MoveController = GameObject.Find("MovementController");
        controller = GetComponent<CharacterController>();

        //LAN Switch
        if (!Network.isClient && !Network.isServer)
            this.enabled = false;
        else if (Network.isClient || Network.isServer)
        {
            if (this.GetComponent<Player1MovementController>())
            {
                self1 = this.GetComponent<Player1MovementController>();
                self1.enabled = false;
                SynthTank.SetActive(true);
                enemy1 = GameObject.Find("Player2(Clone)").GetComponent<Player2MovementController>();
            }
            else
            {
                self2 = this.GetComponent<Player2MovementController>();
                self2.enabled = false;
                OrganicGrunt.SetActive(true);
                enemy2 = GameObject.Find("Player1(Clone)").GetComponent<Player1MovementController>();
            }
        }

        health = 100;
        MaxHealth = 100;
        bulletSize = 1;
        lastLooking = transform.forward;
    }
    void Update()
    {
        if (enemy1 == null && enemy2 == null)
        {
            if (this.GetComponent<Player1MovementController>())
            {
                self1 = this.GetComponent<Player1MovementController>();
                self1.enabled = false;
                enemy1 = GameObject.Find("Player2(Clone)").GetComponent<Player2MovementController>();
            }
            else
            {
                self2 = this.GetComponent<Player2MovementController>();
                self2.enabled = false;
                enemy2 = GameObject.Find("Player1(Clone)").GetComponent<Player1MovementController>();
            }
        }

        if (networkView.isMine)
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
                        transform.Rotate(Vector3.up, xSensitivity * Input.GetAxis("Horizontal"));

                        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) + transform.right * Input.GetAxis("Strafe1");
                        moveDirection = transform.TransformDirection(moveDirection);
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

                if (win == false)
                {
                    if (Input.GetJoystickNames().Length != 0)
                    {
                        if ((Input.GetAxis("360_RightTrigger1") == 1) && reloading == false)
                        {
                            createBullet();
                            reloading = true;
                        }
                    }
                    else
                    {
                        if ((Input.GetAxis("Fire1") == 1) && reloading == false)
                        {
                            createBullet();
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
                    if (Input.GetJoystickNames().Length != 0)
                    {
                        if ((Input.GetAxis("360_RightTrigger1") == 1) && swinging == false)
                        {
                            GameObject sword = Instantiate(Sword, transform.position + 3 * this.transform.forward, transform.rotation) as GameObject;
                            sword.tag = tag;
                            swinging = true;
                            Destroy(sword.gameObject, 0.2f);
                        }
                    }
                    else
                    {
                        if ((Input.GetAxis("Fire1") == 1) && swinging == false)
                        {
                            GameObject sword = Instantiate(Sword, transform.position + 3 * this.transform.forward, transform.rotation) as GameObject;
                            sword.tag = tag;
                            swinging = true;
                            Destroy(sword.gameObject, 0.2f);
                        }
                    }
                }
            }

            healthPieceGreen.GetComponent<Image>().fillAmount = (float)((health * 2) / (MaxHealth * 3));

            if ((float)((health * 2) / (MaxHealth * 3)) <= 0.16f)
            {
                healthPieceGreen.GetComponent<Image>().sprite = healthPieceRed;
            }
            //if (win == true)
            //{
            //    BattleStats.winner = tag;
            //    Destroy(MoveController.GetComponent<PawnMove>().Player02);
            //    if (TurnStateMachine.state == TurnStateMachine.State.playerTurn)
            //    {
            //        TurnStateMachine.state = TurnStateMachine.State.otherTurn;
            //    }
            //    else
            //    {
            //        TurnStateMachine.state = TurnStateMachine.State.playerTurn;
            //    }
            //    Destroy(GameObject.Find("BattleSceneAdditive"));
            //    //Application.LoadLevel("TestingHexTiles");
            //    //Destroy(this.gameObject);
            //}
            //Debug.Log("player win  : " + win);
            //if (health <= 0 && win == false)
            //{
            //    if (MoveController.GetComponent<PawnMove>().Player01.tag == "White")
            //    {
            //        SpawnBasicUnits.WhitePieceCount--;
            //    }
            //    else
            //    {
            //        SpawnBasicUnits.BlackPieceCount--;
            //    }
            //    Destroy(MoveController.GetComponent<PawnMove>().Player01);
            //    if (enemy1 == null)
            //        enemy1.swinging = false;
            //    else if (enemy2 == null)
            //        enemy2.swinging = false;
            //    //Destroy(this.gameObject);
            //}
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (Network.isClient || Network.isServer)
        {

            //If the player gets shot

            if (other.tag == "alienBullet" && Network.isServer)
            {
                Network.Destroy(other.gameObject);
                networkView.RPC("subHealth", RPCMode.AllBuffered);
            }
            else if (other.tag == "robotBullet" && Network.isServer)
            {
                Network.Destroy(other.gameObject);
                networkView.RPC("subHealth", RPCMode.AllBuffered);
            }
            if (other.name == "laser" && other.GetComponent<LaserController>().shooting)
            {
                health -= 0.5f;
            }
            //If the player gets hit with melee
            if (other.name == "Sword(Clone)" && other.tag != tag)
            {
                if(enemy1 == null)
                    enemy1.swinging = false;
                else if (enemy2 == null)
                    enemy2.swinging = false;
                Debug.Log("hit by sword");
                Destroy(other.gameObject);
                Vector3 this2That = new Vector3(this.transform.position.x - other.transform.position.x, 0, this.transform.position.z - other.transform.position.z);
                this.gameObject.GetComponent<CharacterController>().Move(2 * (this2That));
                health -= 10;
            }
        }

    }

    void createBullet()
    {
        Rigidbody bulletClone = Network.Instantiate(Bullet, transform.position + 2f * bulletSize * transform.forward, transform.rotation, 0) as Rigidbody;
        bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
        bulletClone.rigidbody.useGravity = false;
        bulletClone.velocity = transform.TransformDirection(transform.forward * bulletSpeed);
        bulletLifeEnded(bulletClone);
        audio.Play();
        bulletSize = 1;
        bulletSpeed = 25;

        //Rigidbody bulletClone = Instantiate(Bullet, transform.position + 1.5f * bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
        //bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
        //bulletClone.rigidbody.useGravity = false;
        //bulletClone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
        //Destroy(bulletClone.gameObject, 3);
        //audio.Play();
        //bulletSize = 1;
        //bulletSpeed = 25;
        //reloading = true;
    }

    IEnumerator bulletLifeEnded(Rigidbody b)
    {
        yield return new WaitForSeconds(3);
        Network.Destroy(b.gameObject);
    }

    [RPC]
    void subHealth()
    {
        health -= 10;
    }
}
