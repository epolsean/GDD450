using UnityEngine;
using System.Collections;

public class HexGunPlace : MonoBehaviour {

    public GameObject BasicGun;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
        renderer.material.color = Color.blue;
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(BasicGun, this.transform.position, this.transform.rotation);
        }
    }

    void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }
}
