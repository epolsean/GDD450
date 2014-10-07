using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public Rigidbody Bullet;
    public int bulletSpeed = 4;

    public GameObject playerWin;
    public GameObject HealthBar;
    public GameObject HealthBarFill;

    public PlayerManager enemy; 

    int health;
    int bulletSize;
    float chargeTime = 2.0f;

    bool win = false;
    bool reloading = false;
    float reloadTime = 1.0f;

    Behaviour halo;

	// Use this for initialization
	void Start () {

        halo = (Behaviour)GetComponent("Halo");
        health = 100;
        bulletSize = 1;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (reloading)
        {
            reloadTime -= Time.deltaTime;
            if (reloadTime < 0)
            {
                reloadTime = 1.0f;
                reloading = false;
            }
        }

        if (Input.GetAxis("Fire2")==1 && name == "Player 1 Controller" && bulletSize==1 && reloading == false)
        {
            chargeTime -= Time.deltaTime;
            if (chargeTime < 0)
            {
                halo.enabled = true;
                bulletSize = 2;
            }
        }
        else if (Input.GetAxis("Fire3")==1 && name == "Player 2 Controller" && bulletSize==1 && reloading == false)
        {
            Debug.Log(chargeTime);
            chargeTime -= Time.deltaTime;
            if (chargeTime < 0)
            {
                halo.enabled = true;
                bulletSize = 2;
            }
        }

        CharacterController controller = GetComponent<CharacterController>();
        
        if (controller.isGrounded && win == false)
        {
            if (Input.GetButtonUp("Fire2") && name == "Player 1 Controller" && reloading == false)
            {
                Rigidbody bulletClone = Instantiate(Bullet, transform.position + bulletSize * this.transform.forward, transform.rotation) as Rigidbody;
                bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                bulletClone.rigidbody.useGravity = false;
                bulletClone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
                Destroy(bulletClone.gameObject, 3);
                bulletSize = 1;
                chargeTime = 2.0f;
                halo.enabled = false;
                reloading = true;
            }
            else if (Input.GetButtonUp("Fire3")&& name == "Player 2 Controller" && reloading == false)
            {
                Rigidbody bulletClone = Instantiate(Bullet, transform.position + 2 * this.transform.forward, transform.rotation) as Rigidbody;
                bulletClone.gameObject.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                bulletClone.rigidbody.useGravity = false;
                bulletClone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
                Destroy(bulletClone.gameObject, 3);
                bulletSize = 1;
                chargeTime = 2.0f;
                halo.enabled = false;
                reloading = true; 
            }

        }

        HealthBar.GetComponent<Slider>().value = health;

        if (health <= 30)
        {
            HealthBarFill.GetComponent<Image>().color = Color.red;
        }
        if (win == true)
        {
            Destroy(HealthBar);
            Destroy(GameObject.Find("P1 Health Text"));
            Destroy(GameObject.Find("P2 Health Text"));
            Instantiate(playerWin, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        if (health == 0 && win == false)
        {
            enemy.win = true;
            Destroy(this.gameObject);
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
                health -= 20;
            }
        }
    }
}
