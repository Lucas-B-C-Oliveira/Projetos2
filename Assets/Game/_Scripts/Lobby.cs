using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Lobby : MonoBehaviour
{   public static Lobby LobbyGM;

    public GameObject panelLogin, panelLobby;
    public Text lobbyAguardar, lobbyTimeStart;

    public InputField playerInputField;

    public Text playerStatus;

    private void Awake()
    { LobbyGM = this;

        playerInputField.text = "Digite seu Nome";
    }

    void Start()
    {
        lobbyTimeStart.gameObject.SetActive(false);
        PanelLoginActive();

    }

    public void PanelLobbyActive()
    {
        panelLogin.gameObject.SetActive(false);
        panelLobby.gameObject.SetActive(true);
    }

    public void PanelLoginActive()
    {
        panelLogin.gameObject.SetActive(true);
        panelLobby.gameObject.SetActive(false);
    }

    void Update()
    {
        playerStatus.text = PhotonNetwork.NetworkClientState.ToString();
    }
}
