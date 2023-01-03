using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class EscolherModoMultiplayer : MonoBehaviourPunCallbacks
{
    int numeroModo;
    public Text modo;
    public Text descricao;
    public GameObject botao1;
    public GameObject botao2;

    int numeroDificuldade;
    int numeroArena;
    public GameObject modoarenacoop;
    public Text dificuldade;
    public Text arena;
    public GameObject botao3;
    public GameObject botao4;
    public GameObject botao5;
    public GameObject botao6;

    int numeroPontos;
    public GameObject modoUDEP;
    public Text pontosparaganhar;
    public GameObject botao7;
    public GameObject botao8;

    public GameObject avisoJogadores;
    public GameObject iniciandoPartida;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    void Start() {
        numeroModo = 0;
        numeroArena = 0;
        numeroDificuldade = 0;
        numeroPontos = 3;
        ModoUI();
    }

    public void ModoZero() {
        numeroModo = 0;
        numeroArena = 0;
        numeroDificuldade = 0;
        numeroPontos = 3;
        playerProperties["modoarenaarena"] = null;
        playerProperties["modoarenadificuldade"] = null;
        playerProperties["modoUDEPpontos"] = null;
        playerProperties["modojogo"] = null;
        playerProperties["startgame"] = null;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        ModoUI();
    }

    void Update() {
        if (PhotonNetwork.IsMasterClient) {
            botao1.SetActive(true);
            botao2.SetActive(true);
            botao3.SetActive(true);
            botao4.SetActive(true);
            botao5.SetActive(true);
            botao6.SetActive(true);
            botao7.SetActive(true);
            botao8.SetActive(true);
        } else {
            botao1.SetActive(false);
            botao2.SetActive(false);
            botao3.SetActive(false);
            botao4.SetActive(false);
            botao5.SetActive(false);
            botao6.SetActive(false);
            botao7.SetActive(false);
            botao8.SetActive(false);
        }
    }

    void ModoUI() {
        switch (numeroModo)
        {
            case 0:
                modo.text = "Modo Arena Cooperativo";
                descricao.text = "Lute com outros guerreiros contra as moedas!\nAté 3 jogadores";
                ModoArenaCoopUI();
                modoarenacoop.SetActive(true);
                modoUDEP.SetActive(false);
                break;
            case 1:
                modo.text = "Último Dado em Pé";
                descricao.text = "Seja o último em pé em cada rodada para ganhar pontos!\nAté 4 jogadores";
                ModoUDEPUI();
                modoUDEP.SetActive(true);
                modoarenacoop.SetActive(false);
                break;
            default: break;
        }
    }

    void ModoUDEPUI() {
        pontosparaganhar.text = numeroPontos.ToString();
    }

    void ModoArenaCoopUI() {
        switch (numeroDificuldade)
        {
            case 0: dificuldade.text = "Fácil"; break;
            case 1: dificuldade.text = "Médio"; break;
            case 2: dificuldade.text = "Difícil"; break;
            default: break;
        }

        switch (numeroArena)
        {
            case 0: arena.text = "Tradicional"; break;
            case 1: arena.text = "Floresta"; break;
            case 2: arena.text = "Coliseu"; break;
            default: break;
        }
    }

    public void DireitaUDEP() {
        numeroPontos++;

        if (numeroPontos == 8)
            numeroPontos = 3;
        
        playerProperties["modoUDEPpontos"] = numeroPontos;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        ModoUDEPUI();
    }

    public void EsquerdaUDEP() {
        numeroPontos--;

        if (numeroPontos == 2)
            numeroPontos = 7;
        
        playerProperties["modoUDEPpontos"] = numeroPontos;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        ModoUDEPUI();
    }

    public void DireitaDificuldade() {
        switch (numeroDificuldade)
        {
            case 0: numeroDificuldade = 1; break;
            case 1: numeroDificuldade = 2; break;
            case 2: numeroDificuldade = 0; break;
            default: break;
        }

        playerProperties["modoarenadificuldade"] = numeroDificuldade;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        ModoArenaCoopUI();
    }

    public void EsquerdaDificuldade() {
        switch (numeroDificuldade)
        {
            case 0: numeroDificuldade = 2; break;
            case 1: numeroDificuldade = 0; break;
            case 2: numeroDificuldade = 1; break;
            default: break;
        }

        playerProperties["modoarenadificuldade"] = numeroDificuldade;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        ModoArenaCoopUI();
    }

    public void DireitaArena() {
        switch (numeroArena)
        {
            case 0: numeroArena = 1; break;
            case 1: numeroArena = 2; break;
            case 2: numeroArena = 0; break;
            default: break;
        }

        playerProperties["modoarenaarena"] = numeroArena;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        ModoArenaCoopUI();
    }

    public void EsquerdaArena() {
        switch (numeroArena)
        {
            case 0: numeroArena = 2; break;
            case 1: numeroArena = 0; break;
            case 2: numeroArena = 1; break;
            default: break;
        }

        playerProperties["modoarenaarena"] = numeroArena;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        ModoArenaCoopUI();
    }

    public void Direita() {
        switch (numeroModo)
        {
            case 0: numeroModo = 1; break;
            case 1: numeroModo = 0; break;
            default: break;
        }

        playerProperties["modojogo"] = numeroModo;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        ModoUI();
    }

    public void Esquerda() {
        switch (numeroModo)
        {
            case 0: numeroModo = 1; break;
            case 1: numeroModo = 0; break;
            default: break;
        }

        playerProperties["modojogo"] = numeroModo;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        ModoUI();
    }

    public override void OnPlayerPropertiesUpdate (Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps) {
        if (targetPlayer.CustomProperties.ContainsKey("modojogo") && changedProps.ContainsKey("modojogo")) {
            numeroModo = (int)targetPlayer.CustomProperties["modojogo"];
            ModoUI();
        }
        
        if (targetPlayer.CustomProperties.ContainsKey("modoarenadificuldade") && changedProps.ContainsKey("modoarenadificuldade")) {
            numeroDificuldade = (int)targetPlayer.CustomProperties["modoarenadificuldade"];
            ModoArenaCoopUI();
        }
        
        if (targetPlayer.CustomProperties.ContainsKey("modoarenaarena") && changedProps.ContainsKey("modoarenaarena")) {
            numeroArena = (int)targetPlayer.CustomProperties["modoarenaarena"];
            ModoArenaCoopUI();
        }
        
        if (targetPlayer.CustomProperties.ContainsKey("modoUDEPpontos") && changedProps.ContainsKey("modoUDEPpontos")) {
            numeroPontos = (int)targetPlayer.CustomProperties["modoUDEPpontos"];
            ModoUDEPUI();
        }

        if (targetPlayer.CustomProperties.ContainsKey("startgame") && changedProps.ContainsKey("startgame")) {
            SalvarOpcoesDaPartida();
        }
    }

    void SalvarOpcoesDaPartida() {
        iniciandoPartida.SetActive(true);
        PhotonNetwork.CurrentRoom.IsOpen = false;

        PlayerPrefs.SetInt("multiplayer_modo", numeroModo);
        PlayerPrefs.SetInt("multiplayer_dificuldadearena", numeroDificuldade);
        PlayerPrefs.SetInt("multiplayer_arenaarena", numeroArena);
        PlayerPrefs.SetInt("multiplayer_numeroUDEP", numeroPontos);
    }

    public void PlayButton() {
        if (numeroModo == 0 && PhotonNetwork.CurrentRoom.PlayerCount > 3) {
            avisoJogadores.SetActive(true);
            return;
        }

        playerProperties["startgame"] = true;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        SalvarOpcoesDaPartida();

        PhotonNetwork.LoadLevel("MultiplayerCharacterSelect");
    }
}
