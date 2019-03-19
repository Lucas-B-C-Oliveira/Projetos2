using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ThisSceneManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPref;

    private void Start()
    {
        PhotonNetwork.Instantiate(playerPref.name, playerPref.transform.position, playerPref.transform.rotation,0);
    }


    void Update()
    {
        
    }
}
