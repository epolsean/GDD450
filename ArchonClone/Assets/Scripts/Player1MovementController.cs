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

    public GameObject healthPieceGreen;
    public Sprite healthPieceRed;
    public GameObject special;

    public bool speedBoost = false;
    public bool damageBoost = false;
    float powerUpTimer = 5.0f;

    public Camera camMine;
    public Camera camEnemy;

    public bool swinging = false;
    float swingTimer = 0.5f;

    public float specialTimer = 5.0f;
    bool specialAvailable = false;
    public bool usingShield = false;
    float shieldPower = 100f;
    bool shieldOverheat = false;

    GameObject enemy;

    public float health;
    public float MaxHealth;
    int bulletSize;

    public bool win = false;
    bool reloading = false;
    float reloadTime = 0.8f;
    CharacterController controller;

    public GameObject MoveController;

    public bool isAlien;

    void Start()
    {
        if (BattleStats.singlePlayer)
        {
            enemy = GameObject.Find("EnemyAI(Clone)");
        }
        else
        {
            enemy = GameObject.Find("Player2(Clone)");
        }
        MoveController = GameObject.Find("MovementController");
        controller = GetComponent<CharacterController>();

        //health = 100;
        speed = MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Movement;
        health = (float)MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Health;
        MaxHealth = (float)MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().MaxHealth;
        bulletSize = 1;
        lastLooking = transform.forward;

        if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteTank(Clone)")
        {
            special.SetActive(false);
            SynthTank.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteScout(Clone)")
        {
            special.SetActive(false);
            SynthScout.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteRunner(Clone)")
        {
            special.SetActive(false);
            SynthRunner.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteGrunt(Clone)")
        {
            special.SetActive(true);
            SynthGrunt.SetActive(true);
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackTank(Clone)")
        {
            special.SetActive(false);
            OrganicTank.SetActive(true);
            isAlien = true;
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackScout(Clone)")
        {
            special.SetActive(false);
            OrganicScout.SetActive(true);
            isAlien = true;
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackRunner(Clone)")
        {
            special.SetActive(false);
            OrganicRunner.SetActive(true);
            isAlien = true;
        }
        else if (MoveController.GetComponent<PawnMove>().Player01.name == "BlackGrunt(Clone)")
        {
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
                            bulletSize = 1;
                            bulletSpeed = 25;
                            reloading = true;
                        }
                    }
                    else
                    {
                        if ((Input.GetAxis("Fire1")==1) && reloading == false)
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

            //Depending on what the player is their specific special will get called
            if(BattleStats.singlePlayer)
            {
                if (enemy.GetComponent<EnemyAIForBattle>().win == false)
                {
                    if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteTank(Clone)" && win == false)
                    {
                        Debug.Log("getting into single player robot tank special");
                        RobotTankSpecial();
                    }
                    else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteScout(Clone)" && win == false)
                    {
                        RobotScoutSpecial();
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
                        AlienScoutSpecial();
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
                        Debug.Log("getting into non single player robot tank special");
                        RobotTankSpecial();
                    }
                    else if (MoveController.GetComponent<PawnMove>().Player01.name == "WhiteScout(Clone)" && win == false)
                    {
                        RobotScoutSpecial();
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
                        AlienScoutSpecial();
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

            healthPieceGreen.GetComponent<Image>().fillAmount = (float)((health*2) / (MaxHealth*3));

            if ((float)((health * 2) / (MaxHealth * 3)) <= 0.16f)
            {
                healthPieceGreen.GetComponent<Image>().sprite = healthPieceRed;
            }
            if (win == true)
            {
                BattleStats.winner = tag;
                if (TurnStateMachine.state == TurnStateMachine.State.playerTurn/* && GameObject.Find("EnemyAI") == null*/)
                {
                    TurnStateMachine.state = TurnStateMachine.State.otherTurn;
                }
                else
                {
                    TurnStateMachine.state = TurnStateMachine.State.playerTurn;
                    EnemyAI.AIstate = EnemyAI.State.Idle;
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

            if (powerUpTimer < 0)
            {
                damageBoost = false;
                speedBoost = false;
            }
            else
            {

            }
        }
    }

    IEnumerator DamageBoost(int startDamage)
    {
        yield return new WaitForSeconds(5.0f);
        yield return startDamage;
    }

    IEnumerator SpeedBoost(int startSpeed)
    {
        yield return new WaitForSeconds(5.0f);
        yield return startSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "PowerUp")
        {
            float statBoost = Random.Range(0, 100);
            if (statBoost < 40)
            {
                damageBoost = true;
                Debug.Log("Damage Boost p1");
                //MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Damage = StartCoRoutineMoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Damage*2;
                //MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Damage = DamageBoost(MoveController.GetComponent<PawnMove>().Player01.GetComponent<PiecePropScript>().Damage);
            }
            else if (statBoost < 70)
            {
                speedBoost = true;
                Debug.Log("speed Boost p1");
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
            Destroy(other.gameObject);
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
                shieldPower -= MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Damage;
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
                shieldPower -= MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Damage;  
            }
        }
        if (other.name == "laser" && other.GetComponent<LaserController>().shooting)
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
                shieldPower -= MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Damage;
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
    void RobotScoutSpecial()
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
        else if(usingShield)
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
    void AlienScoutSpecial()
    {

    }
    void AlienTankSpecial()
    {
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
                specialAvailable = false;
            }
        }
    }
    void AlienRunnerSpecial()
    {

    }

}
