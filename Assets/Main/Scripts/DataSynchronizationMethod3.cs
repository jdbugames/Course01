using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class DataSynchronizationMethod3 : MonoBehaviour, IOnEventCallback
{

    private void Start ()
    {
        PhotonNetwork.AddCallbackTarget(this);
	}
	

	private void FixedUpdate ()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            PhotonNetwork.RaiseEvent((byte)5, null, new Photon.Realtime.RaiseEventOptions { Receivers = Photon.Realtime.ReceiverGroup.All }, new ExitGames.Client.Photon.SendOptions() { });
        }
        
	}

    public void OnEvent(EventData photonEvent)
    {
        if(photonEvent.Code == 5)
        {
            foreach(PhotonView pv in PhotonNetwork.PhotonViews)
            {
                if(pv.IsMine && pv.gameObject.CompareTag("Player"))
                {
                    PhotonNetwork.Instantiate("Cube", new Vector3(pv.gameObject.transform.position.x, pv.gameObject.transform.position.y + 2, pv.gameObject.transform.position.z), Quaternion.identity);
                    break;
                }
            }
        }
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }


}
