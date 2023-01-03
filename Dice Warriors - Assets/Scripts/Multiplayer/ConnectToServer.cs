using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public GameObject conectando;
    public GameObject falha;

    public void ConnectServer()
    {
        conectando.SetActive(true);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        Loader.Load(Loader.Scene.Multiplayer);
    }

    public override void OnDisconnected (DisconnectCause cause)
    {
        conectando.SetActive(false);
        falha.SetActive(true);
    }
}
