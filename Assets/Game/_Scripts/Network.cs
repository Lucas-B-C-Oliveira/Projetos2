using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Network : MonoBehaviourPunCallbacks
{

    public byte playerRoomMax = 5;

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnected()
    {
        Debug.Log("On Connected");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("On Connected To Master");
        Lobby.LobbyGM.PanelLobbyActive();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string roomName = "Room" + Random.Range(0, 99);
        RoomOptions roomOp = new RoomOptions()
        {
            IsOpen = true,
            IsVisible = true,
            MaxPlayers = playerRoomMax
        };

        PhotonNetwork.CreateRoom(roomName, roomOp, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        foreach (var item in PhotonNetwork.PlayerList)
        {
            if (item.IsMasterClient)
            {
                StartGame();
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount > 0)
        {
            foreach (var item in PhotonNetwork.PlayerList)
            {
                if (item.IsMasterClient)
                {
                    StartGame();
                }
            }
        }
    }

    public void StartGame()
    {

        PhotonNetwork.LoadLevel(1);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("On Disconnected because: " + cause);
        Lobby.LobbyGM.PanelLoginActive();
    }

    public void ButtonCancel()
    {
        PhotonNetwork.Disconnect();
    }

    public void ButtonLogin()
    {
        if (Lobby.LobbyGM.playerInputField.text == "Digite seu Nome")
        {
            string playerRandom = "Player" + Random.Range(0, 100);
            Lobby.LobbyGM.playerInputField.text = playerRandom;
        }

        PhotonNetwork.NickName = Lobby.LobbyGM.playerInputField.text;

        PhotonNetwork.ConnectUsingSettings();
    }
}   
