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
    public Rigidbody missile;
    public GameObject Sword;
    public bool isMelee = false;
    public int bulletSpeed = 25;

    public GameObject healthPiece1;
    public GameObject healthPiece2;

    public Camera camMine;
    public Camera camEnemy;

    public bool swinging = false;
    float swingTimer = 0.5f;

    public float specialTimer = 5.0f;
    bool specialAvailable = false;

    Player1MovementController enemy;

    public float health;
    int bulletSize;

    public bool win = false;
    bool reloading = false;
    float reloadTime = 0.8f;
    CharacterController controller;

    public GameObject MoveController;

    void Start()
    {
        enemy = GameObject.Find("Player1(Clone)").GetComponent<Player1MovementController>();
        MoveController = GameObject.Find("MovementController");
        controller = GetComponent<CharacterController>();

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
        //health = 100;
        health = (float)MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Health;
        bulletSize = 1;
        lastLooking = transform.forward;
    }
    void Update()
    {
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
                        transform.forward = new Vector3(Input.GetAxis("360_HorizontalRightStick2"), 0, Input.GetAxis("360_VerticalRightStick2"));
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
                        transform.forward = new Vector3(Input.GetAxis("Horizontal2"), 0, Input.GetAxis("Vertical2"));
                    }
                    moveDirection = new Vector3(Input.GetAxis("Horizontal2"), 0, Input.GetAxis("Vertical2")) + transform.right * Input.GetAxis("Strafe2");
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

                    moveDirection = new Vector3(Input.GetAxis("Horizontal2"), 0, Input.GetAxis("Vertical2")) + transform.right * Input.GetAxis("Strafe2");
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
                    if ((Input.GetAxis("360_RightTrigger2") == 1) && reloading == false)
                    {
                        Rigidbody bulletClone = Instantiate(Bullet, transform.position + 1.5f * bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
                        bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                        bulletClone.rigidbody.useGravity = false;
                        bulletClone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
                        Destroy(bulletClone.gameObject, 3);
                        audio.Play();
                        bulletSize = 1;
                        bulletSpeed = 25;
                        reloading = true;
                    }
                }
                else
                {
                    if ((Input.GetAxis("Fire2") == 1) && reloading == false)
                    {
                        Rigidbody bulletClone = Instantiate(Bullet, transform.position + 1.5f * bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
                        bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                        bulletClone.rigidbody.useGravity = false;
                        bulletClone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
                        Destroy(bulletClone.gameObject, 3);
                        audio.Play();
                        bulletSize = 1;
                        bulletSpeed = 25;
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
                    swingTimer = 0.2f;
                    swinging = false;
                }
            }

            if (win == false)
            {
                if (Input.GetJoystickNames().Length != 0)
                {
                    if ((Input.GetAxis("360_RightTrigger1") == 1) && swinging == false)
                    {
                        GameObject sword = Instantiate(Sword, transform.position + this.transform.forward, transform.rotation) as GameObject;
                        sword.tag = tag;
                        swinging = true;
                        Destroy(sword.gameObject, 0.2f);
                    }
                }
                else
                {
                    if ((Input.GetAxis("Fire1") == 1) && swinging == false)
                    {
                        GameObject sword = Instantiate(Sword, transform.position + this.transform.forward, transform.rotation) as GameObject;
                        sword.tag = tag;
                        swinging = true;
                        Destroy(sword.gameObject, 0.2f);
                    }
                }
            }
        }

        if (MoveController.GetComponent<PawnMove>().Player02.name == "WhiteTank(Clone)")
        {
            RobotTankSpecial();
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "WhiteScout(Clone)")
        {
            RobotScoutSpecial();
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "WhiteRunner(Clone)")
        {
            RobotRunnerSpecial();
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "WhiteGrunt(Clone)")
        {
            RobotGruntSpecial();
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "BlackTank(Clone)")
        {
            AlienTankSpecial();
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "BlackScout(Clone)")
        {
            AlienScoutSpecial();
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "BlackRunner(Clone)")
        {
            AlienRunnerSpecial();
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "BlackGrunt(Clone)")
        {
            AlienGruntSpecial();
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
            Destroy(GameObject.Find("BattleSceneAdditive"));
            Destroy(MoveController.GetComponent<PawnMove>().Player01);
            MoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().UnitOnTile = MoveController.GetComponent<PawnMove>().Player02;
            MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Health = (int)health;
            //Application.LoadLevel("TestingHexTiles");
            //Destroy(this.gameObject);
        }

        if (health <= 0 && win == false)
        {
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
            //MoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().UnitOnTile = MoveController.GetComponent<PawnMove>().Player01;
            //Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //If the player gets shot
        if (other.tag == "robotBullet")
        {
            Destroy(other.gameObject);
            health -= MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Damage;
        }
        if (other.name == "laser" && other.GetComponent<LaserController>().shooting)
        {
            health -= 0.5f;
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

    void RobotGruntSpecial()
    {

    }
    void RobotScoutSpecial()
    {

    }
    void RobotTankSpecial()
    {
        if (specialAvailable == false)
        {
            specialTimer -= Time.deltaTime;
            if (specialTimer < 0)
            {
                specialTimer = 5.0f;
                specialAvailable = true;
            }
        }
        if (Input.GetAxis("Special1") == 1 && specialAvailable)
        {

            Rigidbody missileClone = Instantiate(missile, transform.position + (1.2f * bulletSize * this.transform.forward) + (1.2f * this.transform.up), transform.rotation) as Rigidbody;
            missileClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
            missileClone.GetComponent<TargetEnemy>().target = enemy.gameObject;
            missileClone.rigidbody.useGravity = false;
            missileClone.velocity = transform.TransformDirection(Vector3.forward * 0.75f * bulletSpeed);
            Destroy(missileClone.gameObject, 20);
            audio.Play();
            audio.Play();
            specialAvailable = false;
        }
    }
    void RobotRunnerSpecial()
    {

    }

    void AlienGruntSpecial()
    {

    }
    void AlienScoutSpecial()
    {

    }
    void AlienTankSpecial()
    {

    }
    void AlienRunnerSpecial()
    {

    }
}
