using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InstancePoolMethod1 : MonoBehaviour, IPunPrefabPool
{
    public string prefabPool = "cube";
    private Queue<GameObject> poolCubos = new Queue<GameObject>();

    private PhotonView pv;

	// Use this for initialization
	private void Start ()
    {
        PhotonNetwork.PrefabPool = this;
		
	}

    public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    {
        print(string.Concat("IpunPrefabPool instanciado: ", prefabId));

        //Filtramos por el objeto que usará el pool 
        if(prefabPool == prefabId)
        {
            //Comprobamos si existe algun objeto en el pool 
            if(poolCubos.Count > 0)
            {
                GameObject cubo = poolCubos.Dequeue();
                cubo.transform.position = position;
                cubo.transform.rotation = rotation;
                cubo.SetActive(true);
                return cubo;
            }
        }

        //Si todos estan activados, no existe ninguno o se trata de un objeto distinto, lo instanciamos  directamente
        //IMPORTANTE: se debe instanciar usando la  que trae Unity por defecto y no la de PhotonNetwork, ya que este metodo se ejecuta en todos los 
        //jugadores de la partida cuando un solo jugador instancia un objeto con PhotonNetwork.Instantiate

        return Instantiate(Resources.Load<GameObject>(prefabId), position, rotation);

    }

    public void Destroy(GameObject gameObject)
    {
        print(string.Concat("IpunPrefabPool destruido: ", gameObject.name));

        //Filtramos por el nombre  del gameobject, al instanciarce un objeto se le añade "Clone" al nombre, por eso no se puede comparar directamente
        if(gameObject.name.StartsWith(prefabPool))
        {
            Destroy(gameObject);
        }
    }
	
}
