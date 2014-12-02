using System;
using System.IO;
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
    bool specialAvailable = true;
    public bool usingShield = false;
    float shieldPower = 100f;
    bool shieldOverheat = false;
    bool boost = false;

    GameObject enemy;

    public float health;
    public float MaxHealth;

    public bool win = false;
    CharacterController controller;

    public GameObject MoveController;

    public bool isAlien;
    public GameObject myCanvas;
    Quaternion canvasRotation;

    float endTimer;

    string enemyName;
    int enemyStartHealth;
    bool printStats = false;

    void Start()
    {
        canvasRotation = new Quaternion(-1,0,0,1);
        myCanvas.transform.rotation = canvasRotation;
        printStats = false;
        if (BattleStats.singlePlayer)
        {
            enemy = GameObject.Find("EnemyBattleAI(Clone)");
        }
        else
        {
            enemy = GameObject.Find("Player2(Clone)");
        }
        MoveController = GameObject.Find("MovementController");
        controller = GetComponent<CharacterController>();

        //controller.collider = GetComponent<BoxCollider>().collider;
        //controller.detectCollisions = false;

        //controller.collider.enabled = false;

        //health = 100;
        speed = MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Movement;
        health = (float)MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Health;
        MaxHealth = (float)MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().MaxHealth;
        attackRate = 2;//MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().AttackRate;
        attackTimer = attackRate;
        bulletSize = 1;
        lastLooking = transform.forward;

        if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteTank(Clone)")
        {
            isMelee = true;
            special.SetActive(true);
            SynthTank.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteScout(Clone)")
        {
            isMelee = false;
            special.SetActive(true);
            SynthScout.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteRunner(Clone)")
        {
            isMelee = false;
            special.SetActive(true);
            SynthRunner.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteGrunt(Clone)")
        {
            isMelee = true;
            special.SetActive(true);
            SynthGrunt.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackTank(Clone)")
        {
            isMelee = true;
            special.SetActive(true);
            OrganicTank.SetActive(true);
            isAlien = true;
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackScout(Clone)")
        {
            isMelee = false;
            special.SetActive(true);
            OrganicScout.SetActive(true);
            isAlien = true;
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackRunner(Clone)")
        {
            isMelee = false;
            special.SetActive(true);
            OrganicRunner.SetActive(true);
            isAlien = true;
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackGrunt(Clone)")
        {
            isMelee = true;
            special.SetActive(true);
            OrganicGrunt.SetActive(true);
            isAlien = true;
        }
        healthPieceGreen.GetComponent<Image>().fillAmount = (float)((health * 2) / (MaxHealth * 3));
        enemyName = MoveController.GetComponent<PawnMove>().Player02.name;
        enemyStartHealth = MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Health;
        Debug.Log("Player 1 damage and speed  : " + MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Damage + "  " + MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Movement);
        Debug.Log("Player 2 damage and speed  : " + MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Damage + "  " + MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Movement);

    }
    void Update()
    {
        if (MoveController.GetComponent<PawnMove>().Player02 != null && MoveController.GetComponent<PawnMove>().Player01 != null)
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
                            if (BattleStats.singlePlayer)
                            {
                                RaycastHit hit;
                                if (Physics.Raycast(GameObject.Find("Hotseat Top Down Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                                {
                                    //target.position = hit.point;
                                    Debug.DrawLine(GameObject.Find("Hotseat Top Down Camera").transform.position, hit.point, Color.red);
                                    transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                                }
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

                            /*transform.Rotate(Vector3.up, xSensitivity * Input.GetAxis("Horizontal"));

                            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) + transform.right * Input.GetAxis("Strafe1");
                            moveDirection = transform.TransformDirection(moveDirection);
                            moveDirection *= speed;*/
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
                        if (Input.GetJoystickNames().Length != 0)
                        {
                            if ((Input.GetAxis("360_RightTrigger1") == 1) && reloading == false)
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
                            if (BattleStats.singlePlayer)
                            {
                                if ((Input.GetMouseButton(0)) && reloading == false)
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
                                if ((Input.GetAxis("Fire1") == 1) && reloading == false)
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
                }
                else
                {
                    if (swinging)
                    {
                        attackTimer -= Time.deltaTime;
                        if (attackTimer < 0)
                        {
                            attackTimer = attackRate;
                            swinging = false;
                        }
                    }

                    if (win == false) // If the player hasnt won yet
                    {
                        if (Input.GetJoystickNames().Length != 0) // If there is a controller connected
                        {
                            if ((Input.GetAxis("360_RightTrigger1") == 1) && swinging == false)
                            {
                                GameObject sword = Instantiate(Sword, transform.position + 3 * this.transform.forward, transform.rotation) as GameObject;
                                sword.tag = tag;
                                swinging = true;
                                Destroy(sword.gameObject, 0.2f);
                            }
                        }
                        else // If no controller connected
                        {
                            if (BattleStats.singlePlayer)
                            {
                                if ((Input.GetMouseButton(0)) && swinging == false)
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
                }
                myCanvas.transform.rotation = canvasRotation;
                //Depending on what the player is their specific special will get called
                if (BattleStats.singlePlayer)
                {
                    if (enemy.GetComponent<EnemyAIForBattle>().win == false)
                    {
                        if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteTank(Clone)" && win == false)
                        {
                            RobotTankSpecial();
                        }
                        else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteScout(Clone)" && win == false)
                        {
                            //RobotScoutSpecial();
                        }
                        else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteRunner(Clone)" && win == false)
                        {
                            RobotRunnerSpecial();
                        }
                        else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteGrunt(Clone)" && win == false)
                        {
                            RobotGruntSpecial();
                        }
                        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackTank(Clone)" && win == false)
                        {
                            AlienTankSpecial();
                        }
                        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackScout(Clone)" && win == false)
                        {
                            //AlienScoutSpecial();
                        }
                        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackRunner(Clone)" && win == false)
                        {
                            AlienRunnerSpecial();
                        }
                        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackGrunt(Clone)" && win == false)
                        {
                            AlienGruntSpecial();
                        }
                    }
                }
                else
                {
                    if (enemy.GetComponent<Player2MovementController>().win == false)
                    {
                        if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteTank(Clone)" && win == false)
                        {
                            RobotTankSpecial();
                        }
                        else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteScout(Clone)" && win == false)
                        {
                            //RobotScoutSpecial();
                        }
                        else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteRunner(Clone)" && win == false)
                        {
                            RobotRunnerSpecial();
                        }
                        else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteGrunt(Clone)" && win == false)
                        {
                            RobotGruntSpecial();
                        }
                        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackTank(Clone)" && win == false)
                        {
                            AlienTankSpecial();
                        }
                        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackScout(Clone)" && win == false)
                        {
                            //AlienScoutSpecial();
                        }
                        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackRunner(Clone)" && win == false)
                        {
                            AlienRunnerSpecial();
                        }
                        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackGrunt(Clone)" && win == false)
                        {
                            AlienGruntSpecial();
                        }
                    }
                }

                healthPieceGreen.GetComponent<Image>().fillAmount = (float)((health * 2) / (MaxHealth * 3));

                if ((float)((health * 2) / (MaxHealth * 3)) <= 0.16f)
                {
                    healthPieceGreen.GetComponent<Image>().sprite = healthPieceRed;
                }
                if (health <= 0 && win == false)
                {
                    MoveController.GetComponent<PawnMove>().Player01.GetComponent<pieceMove>().datSprite.SetActive(false);
                    if (MoveController.GetComponent<PawnMove>().Player01.tag == "White")
                    {
                        SpawnBasicUnits.WhitePieceCount--;
                    }
                    else
                    {
                        SpawnBasicUnits.BlackPieceCount--;
                    }
                    Destroy(MoveController.GetComponent<PawnMove>().Player01);
                    if (BattleStats.singlePlayer)
                    {
                        enemy.GetComponent<EnemyAIForBattle>().win = true;
                    }
                    else
                    {
                        enemy.GetComponent<Player2MovementController>().win = true;
                    }
                    //MoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().UnitOnTile = MoveController.GetComponent<PawnMove>().Player02;
                    //Destroy(this.gameObject);
                }
            }
        }
        if (win == true)
        {
            /*if (!Application.isEditor)
            {
                if (!printStats)
                {  
                    path = Application.dataPath;
                    path += "Resources/WhoWins.txt";
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        sw.WriteLine("Battle between .... " + MoveController.GetComponent<PawnMove>().Player01.name + " with " + MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Health + " health  vs   " + enemyName + " with " + enemyStartHealth + " health");
                        sw.WriteLine("Winner is " + MoveController.GetComponent<PawnMove>().Player01.name);
                        sw.WriteLine("-------------------");
                        UpdateStats();
                        printStats = true;
                    }
                }
            }*/
            //win = false; 
            //TurnStateMachine.fightDone = true; 

            if (endTimer <= 3)
            {
                endTimer += Time.deltaTime;
            }
            else
            {
                TurnStateMachine.fightDone = true;
                BattleStats.winner = tag;
                Destroy(GameObject.Find("BattleSceneAdditive"));
                Destroy(MoveController.GetComponent<PawnMove>().Player02);
                MoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().UnitOnTile = MoveController.GetComponent<PawnMove>().Player01;
                //Application.LoadLevel("TestingHexTiles");
                //Destroy(this.gameObject);
                MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Health = (int)health;
            }
        }
    }

    IEnumerator DamageBoost(int startDamage)
    {
        yield return new WaitForSeconds(4.0f);
        if (MoveController.GetComponent<PawnMove>().Player01 != null)
        {
            MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Damage = startDamage;
        }
    }

    IEnumerator SpeedBoost(int startSpeed)
    {
        yield return new WaitForSeconds(4.0f);
        if (MoveController.GetComponent<PawnMove>().Player01 != null)
        {
            MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Movement = startSpeed;
            speed = MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Movement;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (MoveController.GetComponent<PawnMove>().Player02 != null && MoveController.GetComponent<PawnMove>().Player01 != null)
        {
            if (other.name == "PowerUp")
            {
                float statBoost = UnityEngine.Random.Range(0, 100);
                if (statBoost < 40)
                {
                    Debug.Log("Damage Boost p1");
                    StartCoroutine("DamageBoost", MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Damage);
                    MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Damage = MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Damage * 2;
                }
                else if (statBoost < 70)
                {
                    Debug.Log("Speed Boost p1");
                    StartCoroutine("SpeedBoost", MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Movement);
                    MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Movement = MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Movement * 2;
                    speed = MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Movement;
                }
                else
                {
                    health += 10;
                    if (health > MaxHealth)
                    {
                        health = MaxHealth;
                    }
                    Debug.Log("increase health p1");
                }
                ItemSpawner.numPowerUps--;
                other.transform.parent.gameObject.GetComponent<ItemSpawner>().empty = true;
                other.gameObject.SetActive(false);
            }
            //If the player gets shot
            if (!isAlien && other.tag == "alienBullet")
            {
                Destroy(other.gameObject);
                if (!usingShield)
                {
                    health -= MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Damage;
                }
                else
                {
                    if ((shieldPower - MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Damage) <= 0.1)
                    {
                        usingShield = false;
                        shieldPower = 0.1f;
                    }
                    else
                    {
                        shieldPower -= MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Damage;
                    }
                }
            }
            else if (isAlien && other.tag == "robotBullet")
            {
                Destroy(other.gameObject);
                if (!usingShield)
                {
                    health -= MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Damage;
                }
                else
                {
                    if ((shieldPower - MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Damage) <= 0.1)
                    {
                        usingShield = false;
                        shieldPower = 0.1f;
                    }
                    else
                    {
                        shieldPower -= MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Damage;
                    }
                }
            }
            if (other.tag == "Laser" && other.GetComponent<LaserController>().shooting)
            {
                health -= 0.5f;
            }
            //If the player gets hit with melee
            if (other.name == "Sword(Clone)" && other.tag != tag)
            {
                Destroy(other.gameObject);
                Vector3 this2That = new Vector3(this.transform.position.x - enemy.transform.position.x, 0, this.transform.position.z - enemy.transform.position.z);
                controller.SimpleMove(MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Damage * (Vector3.Normalize(this2That)));
                if (!usingShield)
                {
                    health -= MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Damage;
                }
                else
                {
                    if ((shieldPower - MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Damage) <= 0.1)
                    {
                        usingShield = false;
                        shieldPower = 0.1f;
                    }
                    else
                    {
                        shieldPower -= MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Damage;
                    }
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Laser" && other.GetComponent<LaserController>().shooting)
        {
            health -= 5*Time.deltaTime;
        }
    }

    void RobotGruntSpecial()
    {
        if (shieldPower >= 0.1)
        {
            special.GetComponent<Image>().fillAmount = shieldPower / 100;
        }
        else
        {
            shieldPower = 0.1f;
        }
        Behaviour h = (Behaviour)GetComponent("Halo");
        if (usingShield == false && shieldPower <= 100 && !shieldOverheat)
        {
            shieldPower += Time.deltaTime * 4;
            if (shieldPower > 100)
            {
                shieldPower = 100;
            }
            h.enabled = false;
        }
        else if (usingShield)
        {
            shieldPower -= Time.deltaTime * 10;
            if (shieldPower <= 0.1)
            {
                shieldPower = 0.1f;
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

        if (shieldPower >= 0.1 && !shieldOverheat)
        {
            if (Input.GetJoystickNames().Length != 0) // If there is a controller connected
            {
                if (Input.GetAxis("360_LeftTrigger1") == 1)
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
                if (Input.GetAxis("Special1") == 1)
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

    /*void RobotScoutSpecial()
    {
        if (Input.GetJoystickNames().Length != 0) // If there is a controller connected
        {
            if (Input.GetAxis("360_LeftTrigger1") == 1)
            {
                
            }
        }
        else
        {
            if (Input.GetAxis("Special1") == 1)
            {
                
            }
        }
    }*/

    void RobotTankSpecial()
    {
        if (specialAvailable == false)
        {
            specialTimer -= Time.deltaTime;
            if (specialTimer < 0)
            {
                specialTimer = 10.0f;
                special.GetComponent<Image>().fillAmount = 1.0f;
                specialAvailable = true;
            }
        }
        if (Input.GetJoystickNames().Length != 0) // If there is a controller connected
        {
            if (Input.GetAxis("360_LeftTrigger1") == 1 && specialAvailable)
            {
                Rigidbody missileClone = Instantiate(robotMissile, transform.position + (1.2f * bulletSize * this.transform.forward) + (1.2f * this.transform.up), transform.rotation) as Rigidbody;
                missileClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                missileClone.GetComponent<TargetEnemy>().target = enemy.gameObject;
                missileClone.rigidbody.useGravity = false;
                missileClone.velocity = transform.TransformDirection(Vector3.forward * 0.75f * bulletSpeed);
                Destroy(missileClone.gameObject, 20);
                audio.Play();
                audio.Play();
                special.GetComponent<Image>().fillAmount = 0.001f;
                specialAvailable = false;
            }
        }
        else
        {
            if (Input.GetAxis("Special1") == 1 && specialAvailable)
            {
                Rigidbody missileClone = Instantiate(robotMissile, transform.position + (1.2f * bulletSize * this.transform.forward) + (1.2f * this.transform.up), transform.rotation) as Rigidbody;
                missileClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                missileClone.GetComponent<TargetEnemy>().target = enemy.gameObject;
                missileClone.rigidbody.useGravity = false;
                missileClone.velocity = transform.TransformDirection(Vector3.forward * 0.75f * bulletSpeed);
                Destroy(missileClone.gameObject, 20);
                audio.Play();
                audio.Play();
                special.GetComponent<Image>().fillAmount = 0.001f;
                specialAvailable = false;
            }
        }
    }

    void RobotRunnerSpecial()
    {
        if (specialAvailable)
        {
            if (Input.GetJoystickNames().Length != 0) // If there is a controller connected
            {
                if (Input.GetAxis("360_LeftTrigger1") == 1)
                {
                    if (controller.velocity.magnitude != 0)
                    {
                        controller.Move(moveDirection * 3);
                        boost = true;
                        specialAvailable = false;
                        special.GetComponent<Image>().fillAmount = 0.001f;
                    }
                }
            }
            else
            {
                if (Input.GetAxis("Special1") == 1)
                {
                    if (controller.velocity.magnitude != 0)
                    {
                        controller.Move(moveDirection * 3);
                        boost = true;
                        specialAvailable = false;
                        special.GetComponent<Image>().fillAmount = 0.001f;
                    }
                }
            }
        }
        else
        {
            specialTimer -= Time.deltaTime;
            if (specialTimer < 0)
            {
                specialTimer = 8.0f;
                special.GetComponent<Image>().fillAmount = 1.0f;
                specialAvailable = true;
            }
        }
    }

    void AlienGruntSpecial()
    {
        if (shieldPower >= 0.1)
        {
            special.GetComponent<Image>().fillAmount = shieldPower / 100;
        }
        else
        {
            shieldPower = 0.1f;
        }
        Behaviour h = (Behaviour)GetComponent("Halo");
        if (usingShield == false && shieldPower <= 100 && !shieldOverheat)
        {
            shieldPower += Time.deltaTime * 4;
            if (shieldPower > 100)
            {
                shieldPower = 100;
            }
            h.enabled = false;
        }
        else if (usingShield)
        {
            shieldPower -= Time.deltaTime * 10;
            if (shieldPower <= 0.1)
            {
                shieldPower = 0.1f;
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

        if (shieldPower >= 0.1 && !shieldOverheat)
        {
            if (Input.GetJoystickNames().Length != 0) // If there is a controller connected
            {
                if (Input.GetAxis("360_LeftTrigger1") == 1)
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
                if (Input.GetAxis("Special1") == 1)
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

    /*void AlienScoutSpecial()
    {
        if (Input.GetJoystickNames().Length != 0) // If there is a controller connected
        {
            if (Input.GetAxis("360_LeftTrigger1") == 1)
            {

            }
        }
        else
        {
            if (Input.GetAxis("Special1") == 1)
            {

            }
        }
    }*/

    void AlienTankSpecial()
    {
        if (specialAvailable == false)
        {
            specialTimer -= Time.deltaTime;
            if (specialTimer < 0)
            {
                specialTimer = 10.0f;
                special.GetComponent<Image>().fillAmount = 1.0f;
                specialAvailable = true;
            }
        }
        if (Input.GetJoystickNames().Length != 0) // If there is a controller connected
        {
            if (Input.GetAxis("360_LeftTrigger1") == 1 && specialAvailable)
            {
                Rigidbody missileClone = Instantiate(alienMissile, transform.position + (1.2f * bulletSize * this.transform.forward) + (1.2f * this.transform.up), transform.rotation) as Rigidbody;
                missileClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                missileClone.GetComponent<TargetEnemy>().target = enemy.gameObject;
                missileClone.rigidbody.useGravity = false;
                missileClone.velocity = transform.TransformDirection(Vector3.forward * 0.75f * bulletSpeed);
                Destroy(missileClone.gameObject, 20);
                audio.Play();
                audio.Play();
                special.GetComponent<Image>().fillAmount = 0.001f;
                specialAvailable = false;
            }
        }
        else
        {
            if (Input.GetAxis("Special1") == 1 && specialAvailable)
            {
                Rigidbody missileClone = Instantiate(alienMissile, transform.position + (1.2f * bulletSize * this.transform.forward) + (1.2f * this.transform.up), transform.rotation) as Rigidbody;
                missileClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                missileClone.GetComponent<TargetEnemy>().target = enemy.gameObject;
                missileClone.rigidbody.useGravity = false;
                missileClone.velocity = transform.TransformDirection(Vector3.forward * 0.75f * bulletSpeed);
                Destroy(missileClone.gameObject, 20);
                audio.Play();
                audio.Play();
                special.GetComponent<Image>().fillAmount = 0.001f;
                specialAvailable = false;
            }
        }
    }
    void AlienRunnerSpecial()
    {
        if (specialAvailable)
        {
            Debug.Log("PLayer 1 alien runner can use special");
            if (Input.GetJoystickNames().Length != 0) // If there is a controller connected
            {
                if (Input.GetAxis("360_LeftTrigger1") == 1)
                {
                    if (controller.velocity.magnitude != 0)
                    {
                        controller.Move(moveDirection * 3);
                        boost = true;
                        specialAvailable = false;
                        special.GetComponent<Image>().fillAmount = 0.001f;
                    }
                }
            }
            else
            {
                if (Input.GetAxis("Special1") == 1)
                {
                    if (controller.velocity.magnitude != 0)
                    {
                        controller.Move(moveDirection * 3);
                        boost = true;
                        specialAvailable = false;
                        special.GetComponent<Image>().fillAmount = 0.001f;
                    }
                }
            }
        }
        else
        {
            specialTimer -= Time.deltaTime;
            if (specialTimer < 0)
            {
                specialTimer = 8.0f;
                special.GetComponent<Image>().fillAmount = 1.0f;
                specialAvailable = true;
            }
        }
    }

    void UpdateStats()
    {
        if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteGrunt(Clone)")
        {
            if (enemyName == "BlackGrunt(Clone)")
            {
                BattleStats.RobotGruntWinsVSAlienGrunt++;
            }
            else if (enemyName == "BlackTank(Clone)")
            {
                BattleStats.RobotGruntWinsVSAlienTank++;
            }
            else if (enemyName == "BlackScout(Clone)")
            {
                BattleStats.RobotGruntWinsVSAlienScout++;
            }
            else if (enemyName == "BlackRunner(Clone)")
            {
                BattleStats.RobotGruntWinsVSAlienRunner++;
            }
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteTank(Clone)")
        {
            if (enemyName == "BlackGrunt(Clone)")
            {
                BattleStats.RobotTankWinsVSAlienGrunt++;
            }
            else if (enemyName == "BlackTank(Clone)")
            {
                BattleStats.RobotTankWinsVSAlienTank++;
            }
            else if (enemyName == "BlackScout(Clone)")
            {
                BattleStats.RobotTankWinsVSAlienScout++;
            }
            else if (enemyName == "BlackRunner(Clone)")
            {
                BattleStats.RobotTankWinsVSAlienRunner++;
            }
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteScout(Clone)")
        {
            if (enemyName == "BlackGrunt(Clone)")
            {
                BattleStats.RobotScoutWinsVSAlienGrunt++;
            }
            else if (enemyName == "BlackTank(Clone)")
            {
                BattleStats.RobotScoutWinsVSAlienTank++;
            }
            else if (enemyName == "BlackScout(Clone)")
            {
                BattleStats.RobotScoutWinsVSAlienScout++;
            }
            else if (enemyName == "BlackRunner(Clone)")
            {
                BattleStats.RobotScoutWinsVSAlienRunner++;
            }
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteRunner(Clone)")
        {
            if (enemyName == "BlackGrunt(Clone)")
            {
                BattleStats.RobotRunnerWinsVSAlienGrunt++;
            }
            else if (enemyName == "BlackTank(Clone)")
            {
                BattleStats.RobotRunnerWinsVSAlienTank++;
            }
            else if (enemyName == "BlackScout(Clone)")
            {
                BattleStats.RobotRunnerWinsVSAlienScout++;
            }
            else if (enemyName == "BlackRunner(Clone)")
            {
                BattleStats.RobotRunnerWinsVSAlienRunner++;
            }
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackGrunt(Clone)")
        {
            if (enemyName == "WhiteGrunt(Clone)")
            {
                BattleStats.AlienGruntWinsVSRobotGrunt++;
            }
            else if (enemyName == "WhiteTank(Clone)")
            {
                BattleStats.AlienGruntWinsVSRobotTank++;
            }
            else if (enemyName == "WhiteScout(Clone)")
            {
                BattleStats.AlienGruntWinsVSRobotScout++;
            }
            else if (enemyName == "WhiteRunner(Clone)")
            {
                BattleStats.AlienGruntWinsVSRobotRunner++;
            }
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackTank(Clone)")
        {
            if (enemyName == "WhiteGrunt(Clone)")
            {
                BattleStats.AlienTankWinsVSRobotGrunt++;
            }
            else if (enemyName == "WhiteTank(Clone)")
            {
                BattleStats.AlienTankWinsVSRobotTank++;
            }
            else if (enemyName == "WhiteScout(Clone)")
            {
                BattleStats.AlienTankWinsVSRobotScout++;
            }
            else if (enemyName == "WhiteRunner(Clone)")
            {
                BattleStats.AlienTankWinsVSRobotRunner++;
            }
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackScout(Clone)")
        {
            if (enemyName == "WhiteGrunt(Clone)")
            {
                BattleStats.AlienScoutWinsVSRobotGrunt++;
            }
            else if (enemyName == "WhiteTank(Clone)")
            {
                BattleStats.AlienScoutWinsVSRobotTank++;
            }
            else if (enemyName == "WhiteScout(Clone)")
            {
                BattleStats.AlienScoutWinsVSRobotScout++;
            }
            else if (enemyName == "WhiteRunner(Clone)")
            {
                BattleStats.AlienScoutWinsVSRobotRunner++;
            }
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackRunner(Clone)")
        {
            if (enemyName == "WhiteGrunt(Clone)")
            {
                BattleStats.AlienRunnerWinsVSRobotGrunt++;
            }
            else if (enemyName == "WhiteTank(Clone)")
            {
                BattleStats.AlienRunnerWinsVSRobotTank++;
            }
            else if (enemyName == "WhiteScout(Clone)")
            {
                BattleStats.AlienRunnerWinsVSRobotScout++;
            }
            else if (enemyName == "WhiteRunner(Clone)")
            {
                BattleStats.AlienRunnerWinsVSRobotRunner++;
            }
        }
    }
}
