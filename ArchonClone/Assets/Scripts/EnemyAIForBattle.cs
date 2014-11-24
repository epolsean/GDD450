using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyAIForBattle : MonoBehaviour
{
    Vector3 currentTarget;
    Vector3 nextTarget;
    Vector3 prevTarget;

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

    public float specialTimer = 5.0f;
    bool specialAvailable = false;
    public bool usingShield = false;
    float shieldPower = 100f;
    bool shieldOverheat = false;

    Player1MovementController enemyController;

    float endTimer;

    public float health;
    public float MaxHealth;

    public bool win = false;
    CharacterController controller;

    public GameObject MoveController;

    public bool isAlien;
    GameObject enemy;

    void Start()
    {
        enemyController = GameObject.Find("Player1(Clone)").GetComponent<Player1MovementController>();
        MoveController = GameObject.Find("MovementController");
        controller = GetComponent<CharacterController>();
        enemy = GameObject.Find("Player1(Clone)");

        speed = MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Movement;
        health = (float)MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Health;
        MaxHealth = (float)MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().MaxHealth;
        attackRate = 2;//MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().AttackRate;
        attackTimer = attackRate;
        bulletSize = 1;

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

    // Update is called once per frame
    void Update()
    {
        if (MoveController.GetComponent<PawnMove>().Player02 != null && MoveController.GetComponent<PawnMove>().Player01 != null)
        {
            if (GameObject.Find("Fight") == null)
            {
                RaycastHit hit;
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
                        //Debug.Log("Distance : " + Vector3.Distance(enemy.transform.position, transform.position));
                        if (Vector3.Distance(enemy.transform.position, transform.position) < 30 && reloading == false)
                        {
                            if (Physics.Raycast(transform.position, Vector3.Normalize(enemy.transform.position - transform.position), out hit))
                            {
                                transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
                                if (hit.transform.gameObject == enemy)
                                {
                                    Debug.DrawLine(transform.position, hit.point, Color.red);
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

                    if (win == false)
                    {
                        if (Vector3.Distance(enemy.transform.position, transform.position) < 10 && reloading == false)
                        {
                            if (Physics.Raycast(transform.position, Vector3.Normalize(enemy.transform.position - transform.position), out hit))
                            {
                                transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
                                if (hit.transform.gameObject == enemy)
                                {
                                    Debug.DrawLine(transform.position, hit.point, Color.red);
                                    GameObject sword = Instantiate(Sword, transform.position + 3*transform.forward, transform.rotation) as GameObject;
                                    sword.tag = tag;
                                    swinging = true;
                                    Destroy(sword.gameObject, 0.2f);
                                }
                            }
                        }
                    }
                }
                if (enemy.GetComponent<Player1MovementController>().win == false)
                {
                    //Depending on what the player is their specific special will get called
                    if (MoveController.GetComponent<PawnMove>().Player02.name == "WhiteTank(Clone)" && win == false && enemyController.win == false)
                    {
                        RobotTankSpecial();
                    }
                    else if (MoveController.GetComponent<PawnMove>().Player02.name == "WhiteScout(Clone)" && win == false && enemyController.win == false)
                    {
                        RobotScoutSpecial();
                    }
                    else if (MoveController.GetComponent<PawnMove>().Player02.name == "WhiteRunner(Clone)" && win == false && enemyController.win == false)
                    {
                        RobotRunnerSpecial();
                    }
                    else if (MoveController.GetComponent<PawnMove>().Player02.name == "WhiteGrunt(Clone)" && win == false && enemyController.win == false)
                    {
                        RobotGruntSpecial();
                    }
                    else if (MoveController.GetComponent<PawnMove>().Player02.name == "BlackTank(Clone)" && win == false && enemyController.win == false)
                    {
                        AlienTankSpecial();
                    }
                    else if (MoveController.GetComponent<PawnMove>().Player02.name == "BlackScout(Clone)" && win == false && enemyController.win == false)
                    {
                        AlienScoutSpecial();
                    }
                    else if (MoveController.GetComponent<PawnMove>().Player02.name == "BlackRunner(Clone)" && win == false && enemyController.win == false)
                    {
                        AlienRunnerSpecial();
                    }
                    else if (MoveController.GetComponent<PawnMove>().Player02.name == "BlackGrunt(Clone)" && win == false && enemyController.win == false)
                    {
                        AlienGruntSpecial();
                    }
                }
                healthPieceGreen.GetComponent<Image>().fillAmount = (float)((health * 2) / (MaxHealth * 3));

                if ((float)((health * 2) / (MaxHealth * 3)) <= 0.16f)
                {
                    healthPieceGreen.GetComponent<Image>().sprite = healthPieceRed;
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
                    enemyController.win = true;
                    //MoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().UnitOnTile = MoveController.GetComponent<PawnMove>().Player02;
                    //Destroy(this.gameObject);
                }
            }
        }
        if (win == true)
        {
            if (endTimer <= 3)
            {
                endTimer += Time.deltaTime;
            }
            else
            {
                TurnStateMachine.fightDone = true;
                BattleStats.winner = tag;
                Destroy(GameObject.Find("BattleSceneAdditive"));
                Destroy(MoveController.GetComponent<PawnMove>().Player01);
                MoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().UnitOnTile = MoveController.GetComponent<PawnMove>().Player02;
                //Application.LoadLevel("TestingHexTiles");
                //Destroy(this.gameObject);
                MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Health = (int)health;
            }
        }
    }

    IEnumerator DamageBoost(int startDamage)
    {
        yield return new WaitForSeconds(4.0f);
        MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Damage = startDamage;
    }

    IEnumerator SpeedBoost(int startSpeed)
    {
        yield return new WaitForSeconds(4.0f);
        MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Movement = startSpeed;
        speed = MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Movement;
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
                    Debug.Log("Damage Boost p2");
                    StartCoroutine("DamageBoost", MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Damage);
                    MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Damage = MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Damage * 2;
                }
                else if (statBoost < 70)
                {
                    Debug.Log("Speed Boost p2");
                    StartCoroutine("SpeedBoost", MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Movement);
                    MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Movement = MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Movement * 2;
                    speed = MoveController.GetComponent<PawnMove>().Player02.GetComponent<PiecePropScript>().Movement;
                }
                else
                {
                    health += 10;
                    if (health > MaxHealth)
                    {
                        health = MaxHealth;
                    }
                    Debug.Log("increase health p2");
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
            if (other.tag == "Laser" && other.GetComponent<LaserController>().shooting)
            {
                health -= 0.5f;
            }
            //If the player gets hit with melee
            if (other.name == "Sword(Clone)" && other.tag != tag)
            {
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
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Laser" && other.GetComponent<LaserController>().shooting)
        {
            health -= Time.deltaTime;
        }
    }

    void ChooseNextTarget()
    {
        prevTarget = currentTarget;
        currentTarget = nextTarget;
        Vector3 moveDirection = Vector3.Normalize(currentTarget - transform.position); // Get direction of current target
        transform.forward = new Vector3(moveDirection.x, 0, moveDirection.z);
    }

    void RobotGruntSpecial()
    {
        if (shieldPower >= 0)
        {
            special.GetComponent<Image>().fillAmount = shieldPower / 100;
        }
        Behaviour h = (Behaviour)GetComponent("Halo");
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
            if (Input.GetAxis("360_LeftTrigger2") == 1)
            {
                usingShield = true;
            }
            else
            {
                usingShield = false;
            }
        }
    }
    void RobotScoutSpecial()
    {
        if (Input.GetJoystickNames().Length != 0) // If there is a controller connected
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
        if (Input.GetJoystickNames().Length != 0) // If there is a controller connected
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

        if (Input.GetJoystickNames().Length != 0) // If there is a controller connected
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
