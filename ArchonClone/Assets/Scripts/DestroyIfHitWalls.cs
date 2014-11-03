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
                networkView.RPC("destroyBullet", RPCMode.AllBuffered);
        }
    }

    [RPC]
    void destroyBullet()
    {
        Network.Destroy(gameObject);
    }
}
