using UnityEngine;
using System.Collections;

public class ResourcePlacement : MonoBehaviour {

    public Material mat1;
    public Material mat2;
    public GameObject building1;
    public GameObject building2;
    public GameObject building3;
    public WaveSetup waveController;

    void OnMouseOver()
    {
        if (tag == "SlotOpen")
        {
            renderer.material = mat2;
            if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.Alpha1))
            {
                waveController.numBuilding1++;
                Instantiate(building1,transform.position,Quaternion.identity);
                tag = "SlotClosed";
            }
            if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.Alpha2))
            {
                waveController.numBuilding2++;
                Instantiate(building2, transform.position, Quaternion.identity);
                tag = "SlotClosed";
            }
            if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.Alpha3))
            {
                waveController.numBuilding3++;
                Instantiate(building3, transform.position, Quaternion.identity);
                tag = "SlotClosed";
            }
        }
    }

    void OnMouseExit()
    {
        renderer.material = mat1;
    }
}
