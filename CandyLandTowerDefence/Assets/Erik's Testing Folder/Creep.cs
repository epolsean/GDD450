using UnityEngine;
using System.Collections;

public class Creep : MonoBehaviour {

    public GameObject creepItem;
    GameObject oldCol;
    public int chance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject != oldCol)
        {
            chance = (int)Mathf.Floor(Random.Range(0,100));
            oldCol = col.gameObject;
            if (chance <= 20)
            {
                Instantiate(creepItem, oldCol.transform.position, oldCol.transform.rotation);
            }
        }
    }
}
