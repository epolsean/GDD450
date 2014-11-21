using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player2MovementController : MonoBehaviour
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

    public Rigidbody robotBullet;
    public Rigidbody alienBullet;
    public Rigidbody robotMissile;
    public Rigidbody alienMissile;
    public GameObject Sword;
    public bool isMelee = false;
    public int bulletSpeed = 25;

    bool reloading = false;
    public bool swinging = false;
    float attackRate = 1.5f;
    float attackTimer = 0;
    int bulletSize;

    public GameObject healthPieceGreen;
    public Sprite healthPieceRed;
    public GameObject special;

    public Camera camMine;
    public Camera camEnemy;

    public float specialTimer = 5.0f;
    bool specialAvailable = false;
    public bool usingShield = false;
    float shieldPower = 100f;
    bool shieldOverheat = false;

    Player1MovementController enemy;

    public float health;
    public float MaxHealth;

    public bool win = false;
    CharacterController controller;

    public GameObject MoveController;

    public bool isAlien;

    float endTimer;

    void Start()
    {
        enemy = GameObject.Find("Player1(Clone)").GetComponent<Player1MovementController>();
        MoveController = GameObject.Find("MovementController");
        controller = GetComponent<CharacterController>();

        //health = 100;
        speed = MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Movement;
        health = (float)MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Health;
        MaxHealth = (float)MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().MaxHealth;
        attackRate = MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().MaxHealth;
        attackTimer = attackRate;
        bulletSize = 1;
        lastLooking = transform.forward;

        if (MoveController.GetComponent<PawnMove>().Player02.name == "WhiteTank(Clone)")
        {
            isMelee = true;
            special.SetActive(true);
            SynthTank.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "WhiteScout(Clone)")
        {
            isMelee = false;
            special.SetActive(true);
            SynthScout.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "WhiteRunner(Clone)")
        {
            isMelee = false;
            special.SetActive(true);
            SynthRunner.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "WhiteGrunt(Clone)")
        {
            isMelee = true;
            special.SetActive(true);
            SynthGrunt.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "BlackTank(Clone)")
        {
            isMelee = true;
            special.SetActive(true);
            OrganicTank.SetActive(true);
            isAlien = true;
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "BlackScout(Clone)")
        {
            isMelee = false;
            special.SetActive(true);
            OrganicScout.SetActive(true);
            isAlien = true;
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "BlackRunner(Clone)")
        {
            isMelee = false;
            special.SetActive(true);
            OrganicRunner.SetActive(true);
            isAlien = true;
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "BlackGrunt(Clone)")
        {
            isMelee = true;
            special.SetActive(true);
            OrganicGrunt.SetActive(true);
            isAlien = true;
        }
        healthPieceGreen.GetComponent<Image>().fillAmount = (float)((health * 2) / (MaxHealth * 3));
    }
    void Update()
    {
        if (GameObject.Find("Fight") == null)
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
                    attackTimer -= Time.deltaTime;
                    if (attackTimer < 0)
                    {
                        attackTimer = attackRate;
                        reloading = false;
                    }
                }

                if (win == false)
                {
                    if (Input.GetJoystickNames().Length == 2)
                    {
                        if ((Input.GetAxis("360_RightTrigger2") == 1) && reloading == false)
                        {
                            if (isAlien)
                            {
                                Rigidbody bulletClone = Instantiate(alienBullet, transform.position + 1.2f * bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
                                bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                                bulletClone.rigidbody.useGravity = false;
                                bulletClone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
                                Destroy(bulletClone.gameObject, 3);
                            }
                            else
                            {
                                Rigidbody bulletClone = Instantiate(robotBullet, transform.position + 1.2f * bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
                                bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                                bulletClone.rigidbody.useGravity = false;
                                bulletClone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
                                Destroy(bulletClone.gameObject, 3);
                            }
                            audio.Play();
                            reloading = true;
                        }
                    }
                    else
                    {
                        if ((Input.GetAxis("Fire2") == 1) && reloading == false)
                        {
                            if (isAlien)
                            {
                                Rigidbody bulletClone = Instantiate(alienBullet, transform.position + 1.2f * bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
                                bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                                bulletClone.rigidbody.useGravity = false;
                                bulletClone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
                                Destroy(bulletClone.gameObject, 3);
                            }
                            else
                            {
                                Rigidbody bulletClone = Instantiate(robotBullet, transform.position + 1.2f * bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
                                bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                                bulletClone.rigidbody.useGravity = false;
                                bulletClone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
                                Destroy(bulletClone.gameObject, 3);
                            }
                            audio.Play();
                            reloading = true;
                        }
                    }
                }
            }
            else
            {
                if (swinging)
                {
                    attackTimer -= Time.deltaTime;
                    if (attackTimer < 0)
                    {
                        attackTimer = attackRate;
                        reloading = false;
                    }
                }

                if (win == false)
                {
                    if (Input.GetJoystickNames().Length == 2)
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
        }

        if (MoveController.GetComponent<PawnMove>().Player02.name == "WhiteTank(Clone)" && win == false && enemy.win == false)
        {
            RobotTankSpecial();
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "WhiteScout(Clone)" && win == false && enemy.win == false)
        {
            RobotScoutSpecial();
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "WhiteRunner(Clone)" && win == false && enemy.win == false)
        {
            RobotRunnerSpecial();
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "WhiteGrunt(Clone)" && win == false && enemy.win == false)
        {
            RobotGruntSpecial();
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "BlackTank(Clone)" && win == false && enemy.win == false)
        {
            AlienTankSpecial();
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "BlackScout(Clone)" && win == false && enemy.win == false)
        {
            AlienScoutSpecial();
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "BlackRunner(Clone)" && win == false && enemy.win == false)
        {
            AlienRunnerSpecial();
        }
        else if (MoveController.GetComponent<PawnMove>().Player02.name == "BlackGrunt(Clone)" && win == false && enemy.win == false)
        {
            AlienGruntSpecial();
        }

        healthPieceGreen.GetComponent<Image>().fillAmount = (float)((health * 2) / (MaxHealth * 3));

        if ((float)((health * 2) / (MaxHealth * 3)) <= 0.16f)
        {
            healthPieceGreen.GetComponent<Image>().sprite = healthPieceRed;
        }
        if (win == true)
        {
            if (TurnStateMachine.state == TurnStateMachine.State.playerTurn/* && GameObject.Find("EnemyAI") == null*/)
            {
                TurnStateMachine.state = TurnStateMachine.State.otherTurn;
            }
            else
            {
                TurnStateMachine.state = TurnStateMachine.State.playerTurn;
                EnemyAI.AIstate = EnemyAI.State.Idle;
            }
            if (endTimer <= 3)
            {
                endTimer += Time.deltaTime;
            }
            else
            {
                BattleStats.winner = tag;
                Destroy(GameObject.Find("BattleSceneAdditive"));
                Destroy(MoveController.GetComponent<PawnMove>().Player01);
                MoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().UnitOnTile = MoveController.GetComponent<PawnMove>().Player02;
                MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Health = (int)health;
                //Application.LoadLevel("TestingHexTiles");
                //Destroy(this.gameObject);
            }
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
        if (!isAlien && other.tag == "alienBullet")
        {
            Destroy(other.gameObject);
            if (!usingShield)
            {
                health -= MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Damage;
            }
            else
            {
                shieldPower -= MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Damage;
            }
        }
        else if (isAlien && other.tag == "robotBullet")
        {
            Destroy(other.gameObject);
            if (!usingShield)
            {
                health -= MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Damage;
            }
            else
            {
                shieldPower -= MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Damage;
            }
        }
        if (other.name == "laser" && other.GetComponent<LaserController>().shooting)
        {
            health -= 0.5f;
        }
        //If the player gets hit with melee
        if (other.name == "Sword(Clone)" && other.tag != tag)
        {
            Debug.Log("hit by sword");
            Destroy(other.gameObject);
            Vector3 this2That = new Vector3(this.transform.position.x - enemy.transform.position.x, 0, this.transform.position.z - enemy.transform.position.z);
            controller.SimpleMove(MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Damage * (Vector3.Normalize(this2That)));
            if (!usingShield)
            {
                health -= MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Damage;
            }
            else
            {
                shieldPower -= MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Damage;
            }
        }
    }

    void RobotGruntSpecial()
    {
        if (shieldPower >= 0)
        {
            special.GetComponent<Image>().fillAmount = shieldPower / 100;
        }
        Behaviour h = (Behaviour)GetComponent("Halo");
        Debug.Log("shield power : " + shieldPower);
        if (usingShield == false && shieldPower <= 100 && !shieldOverheat)
        {
            shieldPower += Time.deltaTime;
            if (shieldPower > 100)
            {
                shieldPower = 100;
            }
            h.enabled = false;
        }
        else if (usingShield)
        {
            shieldPower -= Time.deltaTime * 5;
            if (shieldPower <= 1)
            {
                shieldOverheat = true;
                usingShield = false;
            }
            h.enabled = true;
        }
        if (shieldOverheat)
        {
            h.enabled = false;
            shieldPower += 2 * Time.deltaTime;
            if (shieldPower >= 30)
            {
                shieldOverheat = false;
            }
        }

        if (shieldPower >= 1 && !shieldOverheat)
        {
            if (Input.GetJoystickNames().Length == 2) // If there is a controller connected
            {
                if (Input.GetAxis("360_LeftTrigger2") == 1)
                {
                    usingShield = true;
                }
                else
                {
                    usingShield = false;
                }
            }
            else
            {
                if (Input.GetAxis("Special2") == 1)
                {
                    usingShield = true;
                }
                else
                {
                    usingShield = false;
                }
            }
        }
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
                special.GetComponent<Image>().fillAmount = 1;
                specialAvailable = true;
            }
        }
        if (Input.GetJoystickNames().Length == 2) // If there is a controller connected
        {
            if (Input.GetAxis("360_LeftTrigger2") == 1 && specialAvailable)
            {
                Rigidbody missileClone = Instantiate(robotMissile, transform.position + (1.2f * bulletSize * this.transform.forward) + (1.2f * this.transform.up), transform.rotation) as Rigidbody;
                missileClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                missileClone.GetComponent<TargetEnemy>().target = enemy.gameObject;
                missileClone.rigidbody.useGravity = false;
                missileClone.velocity = transform.TransformDirection(Vector3.forward * 0.75f * bulletSpeed);
                Destroy(missileClone.gameObject, 20);
                audio.Play();
                audio.Play();
                special.GetComponent<Image>().fillAmount = 0;
                specialAvailable = false;
            }
        }
        else
        {
            if (Input.GetAxis("Special2") == 1 && specialAvailable)
            {
                Rigidbody missileClone = Instantiate(robotMissile, transform.position + (1.2f * bulletSize * this.transform.forward) + (1.2f * this.transform.up), transform.rotation) as Rigidbody;
                missileClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                missileClone.GetComponent<TargetEnemy>().target = enemy.gameObject;
                missileClone.rigidbody.useGravity = false;
                missileClone.velocity = transform.TransformDirection(Vector3.forward * 0.75f * bulletSpeed);
                Destroy(missileClone.gameObject, 20);
                audio.Play();
                audio.Play();
                special.GetComponent<Image>().fillAmount = 0;
                specialAvailable = false;
            }
        }
    }
    void RobotRunnerSpecial()
    {

    }

    void AlienGruntSpecial()
    {
        if (shieldPower >= 0)
        {
            special.GetComponent<Image>().fillAmount = shieldPower / 100;
        }
        
        Behaviour h = (Behaviour)GetComponent("Halo");
        Debug.Log("shield power : " + shieldPower);
        if (usingShield == false && shieldPower <= 100 && !shieldOverheat)
        {
            shieldPower += Time.deltaTime;
            if (shieldPower > 100)
            {
                shieldPower = 100;
            }
            h.enabled = false;
        }
        else if (usingShield)
        {
            shieldPower -= Time.deltaTime * 5;
            if (shieldPower <= 1)
            {
                shieldOverheat = true;
                usingShield = false;
            }
            h.enabled = true;
        }
        if (shieldOverheat)
        {
            h.enabled = false;
            shieldPower += 2 * Time.deltaTime;
            if (shieldPower >= 30)
            {
                shieldOverheat = false;
            }
        }

        if (shieldPower >= 1 && !shieldOverheat)
        {
            if (Input.GetJoystickNames().Length == 2) // If there is a controller connected
            {
                if (Input.GetAxis("360_LeftTrigger2") == 1)
                {
                    usingShield = true;
                }
                else
                {
                    usingShield = false;
                }
            }
            else
            {
                if (Input.GetAxis("Special2") == 1)
                {
                    usingShield = true;
                }
                else
                {
                    usingShield = false;
                }
            }
        }
    }
    void AlienScoutSpecial()
    {

    }
    void AlienTankSpecial()
    {
        if (specialAvailable == false)
        {
            specialTimer -= Time.deltaTime;
            if (specialTimer < 0)
            {
                specialTimer = 5.0f;
                special.GetComponent<Image>().fillAmount = 1;
                specialAvailable = true;
            }
        }
        if (Input.GetJoystickNames().Length != 0) // If there is a controller connected
        {
            if (Input.GetAxis("360_LeftTrigger2") == 1 && specialAvailable)
            {
                Rigidbody missileClone = Instantiate(alienMissile, transform.position + (1.2f * bulletSize * this.transform.forward) + (1.2f * this.transform.up), transform.rotation) as Rigidbody;
                missileClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                missileClone.GetComponent<TargetEnemy>().target = enemy.gameObject;
                missileClone.rigidbody.useGravity = false;
                missileClone.velocity = transform.TransformDirection(Vector3.forward * 0.75f * bulletSpeed);
                Destroy(missileClone.gameObject, 20);
                audio.Play();
                audio.Play();
                special.GetComponent<Image>().fillAmount = 0;
                specialAvailable = false;
            }
        }
        else
        {
            if (Input.GetAxis("Special2") == 1 && specialAvailable)
            {
                Rigidbody missileClone = Instantiate(alienMissile, transform.position + (1.2f * bulletSize * this.transform.forward) + (1.2f * this.transform.up), transform.rotation) as Rigidbody;
                missileClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                missileClone.GetComponent<TargetEnemy>().target = enemy.gameObject;
                missileClone.rigidbody.useGravity = false;
                missileClone.velocity = transform.TransformDirection(Vector3.forward * 0.75f * bulletSpeed);
                Destroy(missileClone.gameObject, 20);
                audio.Play();
                audio.Play();
                special.GetComponent<Image>().fillAmount = 0;
                specialAvailable = false;
            }
        }
    }
    void AlienRunnerSpecial()
    {

    }
}
