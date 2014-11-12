using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyAIForBattle : MonoBehaviour 
{
    Vector3 currentTarget;
    Vector3 nextTarget;
    Vector3 prevTarget;

    public static int xSensitivity = 3;
    public static int ySensitivity = 3;

    public float speed = 6.0F;

    public Rigidbody Bullet;
    public GameObject Sword;
    public bool isMelee = false;
    public int bulletSpeed = 25;

    public GameObject healthPiece1;
    public GameObject healthPiece2;

    public bool swinging = false;
    float swingTimer = 0.5f;

    GameObject enemy;

    public float health;
    int bulletSize;

    public bool win = false;
    bool reloading = false;
    float reloadTime = 1f;

    public GameObject death;

    void Start()
    {
        enemy = GameObject.Find("Player1(Clone)");
        health = 100f;
    }
	
	// Update is called once per frame
	void Update () 
    {
        RaycastHit hit;

        //if (Physics.SphereCast(transform.position,5f,transform.forward, out hit))
        //{
            //Debug.DrawLine(transform.position, hit.point, Color.red);
            //transform.LookAt(LookAtTarget); 
        //}
        Debug.Log("in update");
        if (isMelee == false)
        {
            if (reloading)
            {
                reloadTime -= Time.deltaTime;
                if (reloadTime < 0)
                {
                    reloadTime = 1f;
                    reloading = false;
                }
            }

            if (win == false)
            {
                Debug.Log("Distance : " + Vector3.Distance(enemy.transform.position, transform.position));
                if (Vector3.Distance(enemy.transform.position,transform.position)<30 && reloading == false)
                {
                    if(Physics.Raycast(transform.position,Vector3.Normalize(enemy.transform.position-transform.position),out hit))
                    {
                        if (hit.transform.gameObject == enemy)
                        {
                            Debug.DrawLine(transform.position, hit.point, Color.red);
                            Rigidbody bulletClone = Instantiate(Bullet, transform.position + 1.5f * bulletSize * Vector3.Normalize(enemy.transform.position - transform.position), transform.rotation) as Rigidbody;
                            bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                            bulletClone.rigidbody.useGravity = false;
                            bulletClone.velocity = transform.TransformDirection(Vector3.Normalize(enemy.transform.position - transform.position) * bulletSpeed);
                            Destroy(bulletClone.gameObject, 3);
                            audio.Play();
                            bulletSize = 1;
                            bulletSpeed = 25;
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
                swingTimer -= Time.deltaTime;
                if (swingTimer < 0)
                {
                    swingTimer = 0.5f;
                    swinging = false;
                }
            }

            if (win == false)
            {
                Debug.Log("Distance : " + Vector3.Distance(enemy.transform.position, transform.position));
                if (Vector3.Distance(enemy.transform.position, transform.position) < 30 && reloading == false)
                {
                    if (Physics.Raycast(transform.position, Vector3.Normalize(enemy.transform.position - transform.position), out hit))
                    {
                        if (hit.transform.gameObject == enemy)
                        {
                            Debug.DrawLine(transform.position, hit.point, Color.red);
                            GameObject sword = Instantiate(Sword, transform.position + Vector3.Normalize(enemy.transform.position - transform.position), transform.rotation) as GameObject;
                            sword.tag = tag;
                            swinging = true;
                            Destroy(sword.gameObject, 0.2f);
                        }
                    }

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
        if (health <= 0 && win == false)
        {
            death.SetActive(true);
            Invoke("OnDeath", 0.3f);
            enemy.GetComponent<Player1MovementController>().win = true;
        }
    }
	

    void OnDeath()
    {
        Destroy(GameObject.Find("alienGrunt"));
        enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {

        //If the enemy gets shot
        if (other.tag == "robotBullet")
        {
            Destroy(other.gameObject);
            health -= 5;
        }
        //If the enemy gets hit with melee
        if (other.name == "Sword(Clone)" && other.tag != tag)
        {
            enemy.GetComponent<Player1MovementController>().swinging = false;
            Debug.Log("hit by sword");
            Destroy(other.gameObject);
            Vector3 this2That = new Vector3(this.transform.position.x - other.transform.position.x, 0, this.transform.position.z - other.transform.position.z);
            this.gameObject.GetComponent<CharacterController>().Move(2 * (this2That));
            health -= 10;
        }

    }

    void ChooseNextTarget()
    {
        prevTarget = currentTarget;
        currentTarget = nextTarget;
        Vector3 moveDirection = Vector3.Normalize(currentTarget - transform.position); // Get direction of current target
        transform.forward = new Vector3(moveDirection.x, 0, moveDirection.z);
    }
}
