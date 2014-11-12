using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialEnemyAI : MonoBehaviour 
{
    public Transform target1;
    public Transform target2;
    public Transform target3;
    public Transform target4;
    public Transform target5;
    public Transform target6;
    public Transform target7;

    Vector3 currentTarget;
    Vector3 nextTarget;

    public static int xSensitivity = 3;
    public static int ySensitivity = 3;

    public float speed = 6.0F;
    public float gravity = 20.0F;

    public bool isMelee = false;
    public int bulletSpeed = 10;

    public GameObject healthPiece1;
    public GameObject healthPiece2;

    public GameObject healthPiece3;
    public GameObject healthPiece4;

    public bool swinging = false;

    public PlayerControllerTutorial enemy;

    public GameObject death;

    int health;

    public bool win = false;

    void Start()
    {
        currentTarget = target3.position;
        health = 100;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (TutorialTextHints.curTutorialStep >= 3 && TutorialTextHints.curTutorialStep != 4)
        {
            Vector3 moveDirection = Vector3.Normalize(currentTarget - transform.position); // Get direction of current target
            GetComponent<CharacterController>().Move(moveDirection * speed * Time.deltaTime); //move towards target

            healthPiece1.GetComponent<Image>().fillAmount = (float)((float)health / 200);
            healthPiece2.GetComponent<Image>().fillAmount = (float)((float)health / 200);
            healthPiece3.GetComponent<Image>().fillAmount = (float)((float)health / 200);
            healthPiece4.GetComponent<Image>().fillAmount = (float)((float)health / 200);

            if (health <= 55 && TutorialTextHints.curTutorialStep == 3)
            {
                TutorialTextHints.curTutorialStep++;
            }

            if (health <= 30)
            {
                healthPiece1.GetComponent<Image>().color = Color.red;
                healthPiece2.GetComponent<Image>().color = Color.red;
                healthPiece3.GetComponent<Image>().color = Color.red;
                healthPiece4.GetComponent<Image>().color = Color.red;
            }
            if (health <= 0 && win == false)
            {
                death.SetActive(true);
                Invoke("OnDeath", 0.3f);
                enemy.win = true;
                
            }
        }
	}

    void OnDeath()
    {
        Destroy(GameObject.Find("alienGrunt"));
        enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "target1")
        {
            int option = Random.Range(1, 2);
            Invoke("ChooseNextTarget", 1.8f);
            if (option == 1)
            {
                nextTarget = target2.position;
            }
            else if (option == 2)
            {
                nextTarget = target6.position;
            }
        }
        else if (other.name == "target2")
        {
            int option = Random.Range(1, 2);
            Invoke("ChooseNextTarget", 1.8f);
            if (option == 1)
            {
                nextTarget = target3.position;
            }
            else if (option == 2)
            {
                nextTarget = target1.position;
            }
        }
        else if (other.name == "target3")
        {
            int option = Random.Range(1, 3);
            Invoke("ChooseNextTarget", 1.8f);
            if (option == 1)
            {
                nextTarget = target4.position;
            }
            else if (option == 2)
            {
                nextTarget = target2.position;
            }
            else if (option == 3)
            {
                nextTarget = target7.position;
            }
        }
        else if (other.name == "target4")
        {
            int option = Random.Range(1, 2);
            Invoke("ChooseNextTarget", 1.8f);
            if (option == 1)
            {
                nextTarget = target5.position;
            }
            else if (option == 2)
            {
                nextTarget = target3.position;
            }
        }
        else if (other.name == "target5")
        {
            int option = Random.Range(1, 2);
            Invoke("ChooseNextTarget", 1.8f);
            if (option == 1)
            {
                nextTarget = target6.position;
            }
            else if (option == 2)
            {
                nextTarget = target4.position;
            }
        }
        else if (other.name == "target6")
        {
            int option = Random.Range(1, 3);
            Invoke("ChooseNextTarget", 1.8f);
            if (option == 1)
            {
                nextTarget = target1.position;
            }
            else if (option == 2)
            {
                nextTarget = target7.position;
            }
            else if (option == 3)
            {
                nextTarget = target5.position;
            }
        }
        else if (other.name == "target7")
        {
            int option = Random.Range(1, 2);
            Invoke("ChooseNextTarget", 1.8f);
            if (option == 1)
            {
                nextTarget = target6.position;
            }
            else if (option == 2)
            {
                nextTarget = target3.position;
            }
        }
        //If the player gets shot
        if (other.tag == "robotBullet")
        {
            Destroy(other.gameObject);
            health -= 5;
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

    void ChooseNextTarget()
    {
        currentTarget = nextTarget;
        Vector3 moveDirection = Vector3.Normalize(currentTarget - transform.position); // Get direction of current target
        transform.forward = new Vector3(moveDirection.x, 0, moveDirection.z);
    }
}
