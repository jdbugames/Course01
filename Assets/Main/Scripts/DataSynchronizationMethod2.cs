using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DataSynchronizationMethod2 : MonoBehaviour
{
    PhotonView pv;


    private void Start ()
    {
        pv = GetComponent<PhotonView>();
	}
	

	private void FixedUpdate ()
    {
		if(pv.IsMine && Input.GetKeyDown(KeyCode.F))
        {
            pv.RPC("PruebaRPC", RpcTarget.AllViaServer);
        }
	}

    [PunRPC]
    private void PruebaRPC()
    {
        if(transform.localScale.x > 1)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = Vector3.one * 1.5f;
        }
    }
}
