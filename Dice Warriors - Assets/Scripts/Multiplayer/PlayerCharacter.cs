using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviourPunCallbacks
{
    public Image playerImage;
    public Text playerName;
    public Text guerreiro;
    public GameObject particulas;
    Player player;

    public Sprite painel;
    public Sprite d4;
    public Sprite d6;
    public Sprite d8;
    public Sprite d12;
    public Sprite d20;
    public int escolha;
    public bool escolhido;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    public void SetPlayerInfo (Player _player)
    {
        playerName.text = _player.NickName;
        player = _player;
        escolha = 0;
        escolhido = false;
    }

    public void ColocarImagem (int numero) {
        switch (numero)
        {
            case 4: escolha = 4; playerImage.sprite = d4; playerProperties["personagem"] = 4; break;
            case 6: escolha = 6; playerImage.sprite = d6; playerProperties["personagem"] = 6; break;
            case 8: escolha = 8; playerImage.sprite = d8; playerProperties["personagem"] = 8; break;
            case 12: escolha = 12; playerImage.sprite = d12; playerProperties["personagem"] = 12; break;
            case 20: escolha = 20; playerImage.sprite = d20; playerProperties["personagem"] = 20; break;
            default: break;
        }

        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void Desconectou() {
        guerreiro.text = "Desconectou";
        playerImage.sprite = painel;
        playerImage.color = new Color(1f, 1f, 1f, 0.5f);
        escolha = 1;
        escolhido = true;
        particulas.SetActive(false);
    }

    public void DivulgarEscolha() {
        switch (escolha)
        {
            case 4: guerreiro.text = "Tessa"; break;
            case 6: guerreiro.text = "Calebe"; break;
            case 8: guerreiro.text = "Octávia"; break;
            case 12: guerreiro.text = "Dominick"; break;
            case 20: guerreiro.text = "Ícaro"; break;
            default: break;
        }

        particulas.SetActive(true);
        escolhido = true;
        FindObjectOfType<AudioManager>().Play("multiplayerselect");
        playerProperties["escolha"] = true;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate (Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps) {
        if (player == targetPlayer && changedProps.ContainsKey("personagem")) {
            UpdatePlayerItem(targetPlayer);
        }

        if (player == targetPlayer && changedProps.ContainsKey("escolha")) {
            switch (escolha)
            {
                case 4: guerreiro.text = "Tessa"; break;
                case 6: guerreiro.text = "Calebe"; break;
                case 8: guerreiro.text = "Octávia"; break;
                case 12: guerreiro.text = "Dominick"; break;
                case 20: guerreiro.text = "Ícaro"; break;
                default: break;
            }

            FindObjectOfType<AudioManager>().Play("multiplayerselect");
            FindObjectOfType<CharactersMultiplayer>().BloquearPersonagem(escolha);
            escolhido = true;
            particulas.SetActive(true);
        }
    }

    void UpdatePlayerItem (Player player) {
        if (player.CustomProperties.ContainsKey("personagem")) {
            switch ((int)player.CustomProperties["personagem"])
            {
                case 4: escolha = 4; playerImage.sprite = d4; break;
                case 6: escolha = 6; playerImage.sprite = d6; break;
                case 8: escolha = 8; playerImage.sprite = d8; break;
                case 12: escolha = 12; playerImage.sprite = d12; break;
                case 20: escolha = 20; playerImage.sprite = d20; break;
                default: break;
            }
        }
    }
}
