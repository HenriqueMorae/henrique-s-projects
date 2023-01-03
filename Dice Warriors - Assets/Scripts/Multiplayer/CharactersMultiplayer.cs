using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class CharactersMultiplayer : MonoBehaviourPunCallbacks
{
    List<PlayerCharacter> playerCharactersList = new List<PlayerCharacter>();
    public PlayerCharacter playerCharacterPrefab;
    public Transform playerCharacterParent;
    PlayerCharacter characterdoPlayerLocal;

    public Button botao1;
    public Button botao2;
    public Button botao3;
    public Button botao4;
    public Button botao5;
    public Button botao6;
    bool escolhido;

    public GameObject carregando;
    bool tacarregandoesperabb;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    void Start() {
        PlayerList();
        escolhido = false;
        tacarregandoesperabb = false;
    }

    void Update() {
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        foreach (PlayerCharacter pessoa in playerCharactersList)
        {
            if (!pessoa.escolhido)
                return;
        }

        if (!tacarregandoesperabb) {
            tacarregandoesperabb = true;
            StartCoroutine("PeraUmPoco");
        }
    }

    IEnumerator PeraUmPoco () {
        yield return new WaitForSeconds(2f);
        carregando.SetActive(true);
        playerProperties["teladecarregamento"] = true;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        
        // ir pro jogo
        PhotonNetwork.LoadLevel("Tradicional");
    }

    public override void OnPlayerPropertiesUpdate (Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps) {
        if (targetPlayer.CustomProperties.ContainsKey("teladecarregamento") && changedProps.ContainsKey("teladecarregamento")) {
            carregando.SetActive(true);
        }
    }

    void PlayerList() {
        if (PhotonNetwork.CurrentRoom == null)
            return;
        
        foreach (KeyValuePair<int,Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerCharacter newPlayerCharacter = Instantiate(playerCharacterPrefab, playerCharacterParent);
            newPlayerCharacter.SetPlayerInfo(player.Value);

            if (player.Value == PhotonNetwork.LocalPlayer) {
                characterdoPlayerLocal = newPlayerCharacter;
            }

            playerCharactersList.Add(newPlayerCharacter);
        }
    }

    public void Tessa() => characterdoPlayerLocal.ColocarImagem(4);
    public void Calebe() => characterdoPlayerLocal.ColocarImagem(6);
    public void Octavia() => characterdoPlayerLocal.ColocarImagem(8);
    public void Dominick() => characterdoPlayerLocal.ColocarImagem(12);
    public void Icaro() => characterdoPlayerLocal.ColocarImagem(20);

    public override void OnPlayerLeftRoom (Player otherPlayer) {
        foreach (PlayerCharacter player in playerCharactersList)
        {
            if (player.playerName.text == otherPlayer.NickName) {
                player.Desconectou();
            }
        }
    }

    public void EscolherPersonagem() {
        botao1.interactable = false;
        botao2.interactable = false;
        botao3.interactable = false;
        botao4.interactable = false;
        botao5.interactable = false;
        botao6.interactable = false;
        escolhido = true;

        characterdoPlayerLocal.DivulgarEscolha();
    }

    public void BloquearPersonagem (int escolhadooutro) {
        if(PlayerPrefs.GetInt("multiplayer_modo") == 1)
            return;
        
        switch (escolhadooutro)
        {
            case 4: botao2.interactable = false; break;
            case 6: botao3.interactable = false; break;
            case 8: botao4.interactable = false; break;
            case 12: botao5.interactable = false; break;
            case 20: botao6.interactable = false; break;
            default: break;
        }

        if (characterdoPlayerLocal.escolha == escolhadooutro && !escolhido) {
            botao1.gameObject.SetActive(false);
        }
    }
}
