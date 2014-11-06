using UnityEngine;
using System.Collections;

public class DestroyIfHitWalls : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wall")
        {
            if (!Network.isClient && !Network.isServer)
                Destroy(gameObject);
            else
                Network.Destroy(gameObject);
        }
    }
}
