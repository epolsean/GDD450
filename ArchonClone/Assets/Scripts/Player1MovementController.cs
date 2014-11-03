using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player1MovementController : MonoBehaviour {

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

    Player2MovementController enemy;

    int health;
    int bulletSize;
    float chargeTime = 2.0f;

    public bool win = false;
    bool reloading = false;
    float reloadTime = 0.8f;

    public GameObject MoveController;

    Behaviour halo;

    void Start()
    {
        enemy = GameObject.Find("Player2(Clone)").GetComponent<Player2MovementController>();
         MoveController = GameObject.Find("MovementController");
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
        if (!Network.isServer && !Network.isClient)
            calledFromUpdatePlayer1();
        else if (Network.isServer)
        {
            networkView.RPC("calledFromUpdatePlayer1", RPCMode.AllBuffered);
            camMine.camera.rect = new Rect(0,0,1,1);
            camEnemy.gameObject.SetActive(false);
        }
    }

    [RPC]
    void calledFromUpdatePlayer1()
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
                    transform.Rotate(Vector3.up, xSensitivity * Input.GetAxis("Horizontal"));

                    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
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

            /*if ((Input.GetAxis("360_RightTrigger1") == 1 || Input.GetAxis("Fire1") == 1) && bulletSize == 1 && reloading == false)
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
                        if (!Network.isClient && !Network.isServer)
                        {
                            Rigidbody bulletClone = Instantiate(Bullet, transform.position + 1.2f * bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
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
                    if ((Input.GetButtonUp("Fire1")) && reloading == false)
                    {
                        if (!Network.isClient && !Network.isServer)
                        {
                            Rigidbody bulletClone = Instantiate(Bullet, transform.position + 1.2f * bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
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

        if (health <= 30)
        {
            healthPiece1.GetComponent<Image>().color = Color.red;
            healthPiece2.GetComponent<Image>().color = Color.red;
        }
        if (win == true)
        {
            BattleStats.winner = tag;
            Destroy(MoveController.GetComponent<PawnMove>().Player02);
            if(TurnStateMachine.state == TurnStateMachine.State.playerTurn)
            {
                TurnStateMachine.state = TurnStateMachine.State.otherTurn;
            }
            else
            {
                TurnStateMachine.state = TurnStateMachine.State.playerTurn;
            }
            Debug.Log("Should destroy ");
            Destroy(GameObject.Find("BattleTestAdditive"));
            Debug.Log("Should destroy again");
            //Application.LoadLevel("TestingHexTiles");
            //Destroy(this.gameObject);
        }
        Debug.Log("player win  : " + win);
        if (health <= 0 && win == false)
        {
            if (MoveController.GetComponent<PawnMove>().Player01.tag == "White")
            {
                SpawnBasicUnits.WhitePieceCount--;
            }
            else
            {
                SpawnBasicUnits.BlackPieceCount--;
            }
            Destroy(MoveController.GetComponent<PawnMove>().Player01);
            enemy.win = true;
            //Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //If the player gets shot
        if (other.tag == "alienBullet")
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
