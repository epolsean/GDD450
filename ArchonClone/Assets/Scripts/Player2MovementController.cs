using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player2MovementController : MonoBehaviour
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

    public GameObject healthPiece1;
    public GameObject healthPiece2;

    public Camera camMine;
    public Camera camEnemy;

    public bool swinging = false;
    float swingTimer = 0.3f;

    Player1MovementController enemy;

    int health;
    int bulletSize;
    float chargeTime = 2.0f;

    public bool win = false;
    bool reloading = false;
    float reloadTime = 0.5f;

    public GameObject MoveController;

    Behaviour halo;

    void Start()
    {
        enemy = GameObject.Find("Player1(Clone)").GetComponent<Player1MovementController>();
        MoveController = GameObject.Find("MovementController");
        //Determine Character and set up stats
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
        if (!Network.isServer && !Network.isClient)
            calledFromUpdatePlayer2();
        else if (Network.isClient)
        {
            networkView.RPC("calledFromUpdatePlayer2", RPCMode.AllBuffered);
            camMine.camera.rect = new Rect(0,0,1,1);
            camEnemy.gameObject.SetActive(false);
        }
    }

    [RPC]
    void calledFromUpdatePlayer2()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (topDownView)
        {
            if (controller.isGrounded)
            {
                if (Input.GetJoystickNames().Length == 2)
                {
                    if (Input.GetAxis("360_HorizontalRightStick2") == 0 && Input.GetAxis("360_VerticalRightStick2") == 0)
                    {
                        transform.forward = lastLooking;
                    }
                    else
                    {
                        if (!Network.isClient)
                        {
                            transform.forward = new Vector3(Input.GetAxis("360_HorizontalRightStick2"), 0, Input.GetAxis("360_VerticalRightStick2"));
                            lastLooking = transform.forward;
                        }
                        else
                            networkView.RPC("updateRotation", RPCMode.AllBuffered, 0);
                    }
                    moveDirection = new Vector3(Input.GetAxis("360_HorizontalLeftStick2"), 0, Input.GetAxis("360_VerticalLeftStick2"));
                    moveDirection *= speed;
                }
                else
                {
                    if (Input.GetAxis("Horizontal2") == 0 && Input.GetAxis("Vertical2") == 0)
                    {
                        transform.forward = lastLooking;
                    }
                    else
                    {
                        if (!Network.isClient)
                        {
                            transform.forward = new Vector3(Input.GetAxis("Horizontal2"), 0, Input.GetAxis("Vertical2"));
                            lastLooking = transform.forward;
                        }
                        else
                            networkView.RPC("updateRotation", RPCMode.AllBuffered, 1);
                    }
                    moveDirection = new Vector3(Input.GetAxis("Horizontal2"), 0, Input.GetAxis("Vertical2"));
                    moveDirection *= speed;
                }

            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
        else
        {
            if (controller.isGrounded)
            {
                if (Input.GetJoystickNames().Length == 2)
                {
                    transform.Rotate(Vector3.up, xSensitivity * Input.GetAxis("360_HorizontalRightStick2"));

                    moveDirection = new Vector3(Input.GetAxis("360_HorizontalLeftStick2"), 0, Input.GetAxis("360_VerticalLeftStick2"));
                    moveDirection = transform.TransformDirection(moveDirection);
                    moveDirection *= speed;
                }
                else
                {
                    transform.Rotate(Vector3.up, xSensitivity * Input.GetAxis("Horizontal2"));

                    moveDirection = new Vector3(Input.GetAxis("Horizontal2"), 0, Input.GetAxis("Vertical2"));
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
                    reloadTime = 0.5f;
                    reloading = false;
                }
            }

            /*if ((Input.GetAxis("360_RBButton2") == 1 || Input.GetAxis("Fire2") == 1) && name == "Player 2 Controller" && bulletSize == 1 && reloading == false)
            {
                Debug.Log(chargeTime);
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
                    if ((Input.GetAxis("360_RightTrigger2") == 1) && reloading == false)
                    {
                        if (!Network.isClient && !Network.isServer)
                        {
                            Rigidbody bulletClone = Instantiate(Bullet, transform.position + 1.5f * bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
                            bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                            bulletClone.rigidbody.useGravity = false;
                            bulletClone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
                            Destroy(bulletClone.gameObject, 3);
                            audio.Play();
                            bulletSize = 1;
                            bulletSpeed = 25;
                            chargeTime = 0.5f;
                            halo.enabled = false;
                            reloading = true;
                        }
                        else
                        {
                            networkView.RPC("createBullet", RPCMode.AllBuffered);
                        }
                    }
                }
                else
                {
                    if ((Input.GetButtonUp("Fire2")) && reloading == false)
                    {
                        if (!Network.isClient && !Network.isServer)
                        {
                            Rigidbody bulletClone = Instantiate(Bullet, transform.position + 1.5f * bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
                            bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                            bulletClone.rigidbody.useGravity = false;
                            bulletClone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
                            Destroy(bulletClone.gameObject, 3);
                            audio.Play();
                            bulletSize = 1;
                            bulletSpeed = 25;
                            chargeTime = 0.5f;
                            halo.enabled = false;
                            reloading = true;
                        }
                        else
                        {
                            networkView.RPC("createBullet", RPCMode.AllBuffered);
                        }
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
                if (Input.GetButtonUp("360_RightTrigger2") && swinging == false)
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

        if (health <= 30)
        {
            healthPiece1.GetComponent<Image>().color = Color.red;
            healthPiece2.GetComponent<Image>().color = Color.red;
        }
        if (win == true)
        {
            BattleStats.winner = tag;
            if (TurnStateMachine.state == TurnStateMachine.State.playerTurn)
            {
                TurnStateMachine.state = TurnStateMachine.State.otherTurn;
            }
            else
            {
                TurnStateMachine.state = TurnStateMachine.State.playerTurn;
            }
            Destroy(GameObject.Find("BattleTestAdditive"));
            Destroy(MoveController.GetComponent<PawnMove>().Player01);
            //Application.LoadLevel("TestingHexTiles");
            //Destroy(this.gameObject);
        }
        if (health <= 0 && win == false)
        {
            Debug.Log("win b4 : " + enemy.win);
            if (MoveController.GetComponent<PawnMove>().Player02.tag == "White")
            {
                SpawnBasicUnits.WhitePieceCount--;
            }
            else
            {
                SpawnBasicUnits.BlackPieceCount--;
            }
            Destroy(MoveController.GetComponent<PawnMove>().Player02);
            enemy.win = true;
            Debug.Log("win after : " + enemy.win);
            //Destroy(this.gameObject);
        }
    }

    [RPC]
    void updateRotation(int mode)
    {
        if (mode == 0)
        {
            transform.forward = new Vector3(Input.GetAxis("360_HorizontalRightStick2"), 0, Input.GetAxis("360_VerticalRightStick2"));
            lastLooking = transform.forward;
        }
        else
        {
            transform.forward = new Vector3(Input.GetAxis("Horizontal2"), 0, Input.GetAxis("Vertical2"));
            lastLooking = transform.forward;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //If the player gets shot
        if (other.tag == "robotBullet")
        {
            if (!Network.isClient && !Network.isServer)
            {
                Destroy(other.gameObject);
                health -= 10;
            }
            else
                networkView.RPC("destroyBullet", RPCMode.AllBuffered, other.gameObject);
        }
        //If the player gets hit with melee
        if (other.name == "Sword(Clone)" && other.tag != tag)
        {
            enemy.swinging = false;
            Debug.Log("hit by sword");
            Destroy(other.gameObject);
            Vector3 this2That = new Vector3(this.transform.position.x - other.transform.position.x, 0, this.transform.position.z - other.transform.position.z);
            this.gameObject.GetComponent<CharacterController>().Move(2 * (this2That));
            health -= 10;
        }

    }

    [RPC]
    void destroyBullet(GameObject other)
    {
        Network.Destroy(other.gameObject);
        health -= 10;
    }

    [RPC]
    void createBullet()
    {
        Rigidbody bulletClone = Network.Instantiate(Bullet, transform.position + 1.2f * bulletSize * this.transform.forward, transform.rotation, 1) as Rigidbody;
        bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
        bulletClone.rigidbody.useGravity = false;
        bulletClone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
        Destroy(bulletClone.gameObject, 3);
        audio.Play();
        bulletSize = 1;
        bulletSpeed = 25;
        chargeTime = 0.5f;
        halo.enabled = false;
        reloading = true;
    }
}
