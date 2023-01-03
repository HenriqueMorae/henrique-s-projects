using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateOrJoinRoom : MonoBehaviourPunCallbacks
{
    public InputField nome;
    public InputField codigo;
    public Button criar;
    public Button voltar;
    public Button entrar;
    public GameObject criando;
    public GameObject entrando;
    public GameObject avisoNome;
    public GameObject avisoCodigo;
    public GameObject falhaCriar;
    public GameObject falhaEntrar;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public Text nomedasala;

    List<PlayerItem> playerItemsList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;
    bool localPronto;
    PlayerItem itemdoPlayerLocal;
    public Text botaoPronto;
    public Image botaoProntoImage;
    public GameObject jogar;

    public EscolherModoMultiplayer emm;

    void Start() {
        localPronto = false;
        PhotonNetwork.JoinLobby();
    }

    void Update() {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 1 && TodosProntos()) {
            jogar.SetActive(true);
        } else {
            jogar.SetActive(false);
        }
    }

    bool TodosProntos() {
        bool estaotodosprontos = true;

        foreach (KeyValuePair<int,Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            if (player.Value.CustomProperties.ContainsKey("tapronto")) {
                if ((bool)player.Value.CustomProperties["tapronto"]) {

                } else {
                    estaotodosprontos = false;
                }
            } else {
                estaotodosprontos = false;
            }
        }

        return estaotodosprontos;
    }

    public void Desconectar() {
        PhotonNetwork.Disconnect();
    }

    public void CriarSala() {
        avisoCodigo.SetActive(false);
        avisoNome.SetActive(false);
        criando.SetActive(true);
        criar.enabled = false;
        voltar.enabled = false;
        entrar.enabled = false;
        nome.interactable = false;
        codigo.interactable = false;

        if (String.IsNullOrWhiteSpace(nome.text)) {
            criar.enabled = true;
            voltar.enabled = true;
            entrar.enabled = true;
            nome.interactable = true;
            codigo.interactable = true;
            criando.SetActive(false);
            avisoNome.SetActive(true);
            return;
        }

        PhotonNetwork.NickName = nome.text;

        string codigodasala = UnityEngine.Random.Range(10000,100000).ToString();
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.BroadcastPropsChangeToAll = true;
        PhotonNetwork.CreateRoom(codigodasala, options);
    }

    public override void OnCreateRoomFailed (short returnCode, string message) {
        criando.SetActive(false);
        falhaCriar.SetActive(true);
        criar.enabled = true;
        voltar.enabled = true;
        entrar.enabled = true;
        nome.interactable = true;
        codigo.interactable = true;
    }

    public void EntrarSala() {
        avisoCodigo.SetActive(false);
        avisoNome.SetActive(false);
        entrando.SetActive(true);
        criar.enabled = false;
        voltar.enabled = false;
        entrar.enabled = false;
        nome.interactable = false;
        codigo.interactable = false;

        if (String.IsNullOrWhiteSpace(nome.text)) {
            criar.enabled = true;
            voltar.enabled = true;
            entrar.enabled = true;
            nome.interactable = true;
            codigo.interactable = true;
            entrando.SetActive(false);
            avisoNome.SetActive(true);
            return;
        }

        if (String.IsNullOrWhiteSpace(codigo.text)) {
            criar.enabled = true;
            voltar.enabled = true;
            entrar.enabled = true;
            nome.interactable = true;
            codigo.interactable = true;
            entrando.SetActive(false);
            avisoCodigo.SetActive(true);
            return;
        }

        PhotonNetwork.NickName = nome.text;

        PhotonNetwork.JoinRoom(codigo.text);
    }

    public override void OnJoinRoomFailed (short returnCode, string message) {
        entrando.SetActive(false);
        falhaEntrar.SetActive(true);
        criar.enabled = true;
        voltar.enabled = true;
        entrar.enabled = true;
        nome.interactable = true;
        codigo.interactable = true;
    }

    public override void OnJoinedRoom() {
        lobbyPanel.SetActive(false);
        UpdatePlayerList();
        nomedasala.text = "Código da Sala: " + PhotonNetwork.CurrentRoom.Name;
        criando.SetActive(false);
        entrando.SetActive(false);
        roomPanel.SetActive(true);
    }

    public void SairDaSala() {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.JoinLobby();
        criar.enabled = true;
        voltar.enabled = true;
        entrar.enabled = true;
        nome.interactable = true;
        codigo.interactable = true;
    }

    public override void OnLeftRoom() {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    void UpdatePlayerList() {
        localPronto = false;
        botaoPronto.text = "Pronto!";
        botaoProntoImage.color = Color.white;
        emm.ModoZero();

        foreach (PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();

        if (PhotonNetwork.CurrentRoom == null)
            return;
        
        foreach (KeyValuePair<int,Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);
            newPlayerItem.NaoProntoUI();

            if (player.Value == PhotonNetwork.MasterClient) {
                newPlayerItem.AtivarMaster();
            }

            if (player.Value == PhotonNetwork.LocalPlayer) {
                newPlayerItem.CorDiferente();
                itemdoPlayerLocal = newPlayerItem;
            }

            playerItemsList.Add(newPlayerItem);
        }
    }

    public override void OnPlayerEnteredRoom (Player newPlayer) {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom (Player otherPlayer) {
        UpdatePlayerList();
    }

    public void ProntoOuNao () {
        if (localPronto) {
            // nao
            localPronto = false;
            botaoPronto.text = "Pronto!";
            botaoProntoImage.color = Color.white;
            itemdoPlayerLocal.NaoProntoUI();
        } else {
            // pronto
            localPronto = true;
            botaoPronto.text = "Espera!";
            botaoProntoImage.color = Color.red;
            itemdoPlayerLocal.ProntoUI();
        }
    }
}
