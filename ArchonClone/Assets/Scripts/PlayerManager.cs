using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public Rigidbody Bullet;
    public GameObject Sword;
    public bool isMelee = false;
    public int bulletSpeed = 10;

    public GameObject playerWin;
    public GameObject HealthBar;
    public GameObject HealthBarFill;

    bool swinging = false;
    float swingTimer = 0.3f;

    public PlayerManager enemy; 

    int health;
    int bulletSize;
    float chargeTime = 2.0f;

    bool win = false;
    bool reloading = false;
    float reloadTime = 0.5f;

    Behaviour halo;

	// Use this for initialization
	void Start () {

        halo = (Behaviour)GetComponent("Halo");
        health = 100;
        bulletSize = 1;
	
	}
	
	// Update is called once per frame
	void Update () {

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

            if (Input.GetAxis("360_RBButton1") == 1 && name == "Player 1 Controller" && bulletSize == 1 && reloading == false)
            {
                chargeTime -= Time.deltaTime;
                if (chargeTime < 0)
                {
                    halo.enabled = true;
                    bulletSize = 2;
                    bulletSpeed -= 4;
                }
            }
            else if (Input.GetAxis("360_RBButton2") == 1 && name == "Player 2 Controller" && bulletSize == 1 && reloading == false)
            {
                Debug.Log(chargeTime);
                chargeTime -= Time.deltaTime;
                if (chargeTime < 0)
                {
                    halo.enabled = true;
                    bulletSize = 2;
                    bulletSpeed -= 4;
                }
            }

            if (win == false)
            {
                if (Input.GetButtonUp("360_RBButton1") && name == "Player 1 Controller" && reloading == false)
                {
                    Rigidbody bulletClone = Instantiate(Bullet, transform.position + 0.9f*bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
                    bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                    bulletClone.rigidbody.useGravity = false;
                    bulletClone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
                    Destroy(bulletClone.gameObject, 3);
                    audio.Play();
                    bulletSize = 1;
                    bulletSpeed = 10;
                    chargeTime = 0.5f;
                    halo.enabled = false;
                    reloading = true;
                }
                else if (Input.GetButtonUp("360_RBButton2") && name == "Player 2 Controller" && reloading == false)
                {
                    Rigidbody bulletClone = Instantiate(Bullet, transform.position + 0.9f*bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
                    bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                    bulletClone.rigidbody.useGravity = false;
                    bulletClone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
                    Destroy(bulletClone.gameObject, 3);
                    audio.Play();
                    bulletSize = 1;
                    bulletSpeed = 10;
                    chargeTime = 0.5f;
                    halo.enabled = false;
                    reloading = true;
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
                if (Input.GetButtonUp("360_RBButton1") && name == "Player 1 Controller" && swinging == false)
                {
                    GameObject sword = Instantiate(Sword, transform.position + this.transform.forward, transform.rotation) as GameObject;
                    sword.tag = tag;
                    swinging = true;
                    Destroy(sword.gameObject, 0.3f);
                }
                if (Input.GetButtonUp("360_RBButton2") && name == "Player 2 Controller" && swinging == false)
                {
                    GameObject sword = Instantiate(Sword, transform.position + this.transform.forward, transform.rotation) as GameObject;
                    sword.tag = tag;
                    swinging = true;
                    Destroy(sword.gameObject, 0.3f);
                }
            }
        }

        

        HealthBar.GetComponent<Slider>().value = health;

        if (health <= 30)
        {
            HealthBarFill.GetComponent<Image>().color = Color.red;
        }
        if (win == true)
        {
            if (tag == "Player1")
            {
                BattleStats.winner = tag;
            }
            else
            {
                BattleStats.winner = "Player2";
            }
            Destroy(HealthBar);
            Destroy(GameObject.Find("P1 Health Text"));
            Destroy(GameObject.Find("P2 Health Text"));
            //Instantiate(playerWin, transform.position, transform.rotation);
            //Destroy(this.gameObject);
        }
        if (health <= 0 && win == false)
        {
            enemy.win = true;
            //Destroy(this.gameObject);
            Destroy(HealthBar);
        }
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bullet(Clone)")
        {
            Destroy(other.gameObject);
            if (other.transform.localScale.x == 1)
            {
                health -= 5;
            }
            else if (other.transform.localScale.x == 2)
            {
                health -= 15;
            }
        }
        if (other.name == "Sword(Clone)" && other.tag != tag)
        {
            enemy.swinging = false;
            Debug.Log("hit by sword");
            Destroy(other.gameObject);
            Vector3 this2That = new Vector3(this.transform.position.x - other.transform.position.x,0,this.transform.position.z - other.transform.position.z);
            this.gameObject.GetComponent<CharacterController>().Move(2*(this2That));
            health -= 10;
        }
        
    }
}
