using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InstanceManualMethod1 : MonoBehaviour
{
    public KeyCode tecla = KeyCode.F;
    public GameObject objetoAInstanciar;

    private PhotonView pv;

	// Use this for initialization
	private void Start ()
    {
        pv = GetComponent<PhotonView>();
		
	}
	
	// Update is called once per frame
	private void Update ()
    {
        if(pv.IsMine && Input.GetKeyDown(tecla))
        {
            pv.RPC("InstanciarObjeto", RpcTarget.AllViaServer);
        }
		
	}

    [PunRPC]
    private void InstanciarObjeto()
    {
        GameObject instancia = Instantiate(objetoAInstanciar, new Vector3(pv.gameObject.transform.position.x, pv.gameObject.transform.position.y + 2, pv.gameObject.transform.position.z), Quaternion.identity);
        PhotonNetwork.AllocateViewID(instancia.GetPhotonView());
        print(string.Concat("Se ha instanciado de forma automática el objeto ", instancia.name, "con ViewID", instancia.GetPhotonView().ViewID));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PhotonView>() != null)
        {
            Destroy(collision.gameObject);
        }
    }
}
