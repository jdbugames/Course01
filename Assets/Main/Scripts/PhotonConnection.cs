using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class PhotonConnection : MonoBehaviourPunCallbacks {


    public string nombrePrefabEnResources = "Car";

    public void Start()
    {
        //Intentamos conectarnos a Photon Cloud si no estamos ya conectados
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();
        }
        //Si estamos conectados nos unimos a una habitación aleatoria
        else
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    //Se ejecuta cuando se conecta con el servidor
    public override void OnConnectedToMaster()
    {        
        //Nos unimos a una partida
        PhotonNetwork.JoinRandomRoom();
    }

    //Se ejecuta cuando falla el proceso de unirse a una partida aleatoria (normalmente es porque no existe ninguna)
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //Como suele ejecutarse OnJoinRandomFailed cuando no existe ninguna partida, creamos una nueva
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    //Se ejecuta cuando nos unimos a la partida
    public override void OnJoinedRoom()
    {
        //Instanciamos el personaje que va a usar el jugador, el prefab debe estar en la carpeta resources para poder instanciarlo de esta forma
        GameObject car = PhotonNetwork.Instantiate(nombrePrefabEnResources, new Vector3(25f * PhotonNetwork.PlayerList.Length, 5f, 0f), Quaternion.identity, 0);
        //Habilitamos todos sus componentes
        foreach(MonoBehaviour m in car.GetComponents<MonoBehaviour>())
        {
            if (!m.enabled)
                m.enabled = true;
        }
        
    }    


}
