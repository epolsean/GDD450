using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player1MovementController : MonoBehaviour 
{
    public GameObject SynthTank;
    public GameObject SynthScout;
    public GameObject SynthRunner;
    public GameObject SynthGrunt;

    public GameObject OrganicTank;
    public GameObject OrganicScout;
    public GameObject OrganicRunner;
    public GameObject OrganicGrunt;

    public static int xSensitivity = 3;
    public static int ySensitivity = 3;

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

    public GameObject healthPieceGreen;
    public Sprite healthPieceRed;

    public Camera camMine;
    public Camera camEnemy;

    public bool swinging = false;
    float swingTimer = 0.5f;

    public float specialTimer = 5.0f;
    bool specialAvailable = false;

    Player2MovementController enemy;

    public float health;
    public float MaxHealth;
    int bulletSize;

    public bool win = false;
    bool reloading = false;
    float reloadTime = 0.8f;
    CharacterController controller;

    public GameObject MoveController;

    void Start()
    {
        enemy = GameObject.Find("Player2(Clone)").GetComponent<Player2MovementController>();
        MoveController = GameObject.Find("MovementController");
        controller = GetComponent<CharacterController>();

        //health = 100;
        health = (float)MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Health;
        MaxHealth = (float)MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().MaxHealth;
        bulletSize = 1;
        lastLooking = transform.forward;

        if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteTank(Clone)")
        {
            SynthTank.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteScout(Clone)")
        {
            SynthScout.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteRunner(Clone)")
        {
            SynthRunner.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteGrunt(Clone)")
        {
            SynthGrunt.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackTank(Clone)")
        {
            OrganicTank.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackScout(Clone)")
        {
            OrganicScout.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackRunner(Clone)")
        {
            OrganicRunner.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackGrunt(Clone)")
        {
            OrganicGrunt.SetActive(true);
        }
    }
    void Update()
    {
        if (GameObject.Find("Fight") == null)
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
                            Rigidbody bulletClone = Instantiate(Bullet, transform.position + 1.2f * bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
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
                        if ((Input.GetAxis("Fire1")==1) && reloading == false)
                        {
                            Rigidbody bulletClone = Instantiate(Bullet, transform.position + 1.2f * bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
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
                        swingTimer = 0.5f;
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

            if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteTank(Clone)")
            {
                RobotTankSpecial();
            }
            else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteScout(Clone)")
            {
                RobotScoutSpecial();
            }
            else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteRunner(Clone)")
            {
                RobotRunnerSpecial();
            }
            else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteGrunt(Clone)")
            {
                RobotGruntSpecial();
            }
            else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackTank(Clone)")
            {
                AlienTankSpecial();
            }
            else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackScout(Clone)")
            {
                AlienScoutSpecial();
            }
            else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackRunner(Clone)")
            {
                AlienRunnerSpecial();
            }
            else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackGrunt(Clone)")
            {
                AlienGruntSpecial();
            }

            healthPieceGreen.GetComponent<Image>().fillAmount = (float)((health*2) / (MaxHealth*3));

            if ((float)((health * 2) / (MaxHealth * 3)) <= 0.16f)
            {
                healthPieceGreen.GetComponent<Image>().sprite = healthPieceRed;
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
                Destroy(MoveController.GetComponent<PawnMove>().Player02);
                MoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().UnitOnTile = MoveController.GetComponent<PawnMove>().Player01;
                //Application.LoadLevel("TestingHexTiles");
                //Destroy(this.gameObject);
                MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Health = (int)health;
            }

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
                //MoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().UnitOnTile = MoveController.GetComponent<PawnMove>().Player02;
                //Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //If the player gets shot
        if (other.tag == "alienBullet")
        {
            Destroy(other.gameObject);
            health -= MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Damage;
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
