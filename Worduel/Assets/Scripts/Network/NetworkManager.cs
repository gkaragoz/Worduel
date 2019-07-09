﻿using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks {
    
    private void Start() {
        string username = "Player " + Random.Range(1000, 10000);

        PhotonNetwork.LocalPlayer.NickName = username;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        Debug.Log("OnConnectedToMaster");
    }

    public override void OnDisconnected(DisconnectCause cause) {
        Debug.Log("OnDisconnected: " + cause);
    }

    private void OnApplicationQuit() {
        if (PhotonNetwork.IsConnected) {
            PhotonNetwork.Disconnect();
        }
    }

}
