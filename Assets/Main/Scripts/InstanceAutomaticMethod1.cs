using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InstanceAutomaticMethod1 : MonoBehaviour
{
    public KeyCode tecla = KeyCode.F;
    public string objetoInstanciado = "Cube";

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
            GameObject go = PhotonNetwork.Instantiate(objetoInstanciado, new Vector3(pv.gameObject.transform.position.x, pv.gameObject.transform.position.y + 2, pv.gameObject.transform.position.z), Quaternion.identity);
            print(string.Concat("Se ha instanciado de forma automática el objeto ", go.name, "con ViewID", go.GetPhotonView().ViewID));
        }
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PhotonView>() != null)
        {
            PhotonNetwork.Destroy(collision.gameObject);
        }
    }
}
