using UnityEngine;
using System.Collections;

public class DestroyTowerSelect : MonoBehaviour {

    public GameObject HexSelected;

    public GameObject CCS;

	// Use this for initialization
	void Start () {

        CCS = GameObject.Find("FirstPersonCannonControl");
        HexSelected = CCS.GetComponent<CannonCameraSwitch>().TileSelected;
        HexSelected.GetComponent<HexGunPlaceV2>().SpawnTowerAndGun();
        HexGunPlaceV2.anyTileSelected = false;
        HexSelected.GetComponent<HexGunPlaceV2>().thisTileSelected = false;
        HexSelected.transform.renderer.material.color = Color.white;
        Destroy(this.transform.parent.gameObject);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
