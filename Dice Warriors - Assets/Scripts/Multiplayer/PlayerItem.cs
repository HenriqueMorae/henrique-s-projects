using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    public Text playerName;
    public GameObject pronto;
    public GameObject master;
    public Image botaoImage;
    Player player;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    public void SetPlayerInfo (Player _player)
    {
        playerName.text = _player.NickName;
        player = _player;
    }

    public void AtivarMaster() {
        master.SetActive(true);
    }

    public void CorDiferente() {
        botaoImage.color = new Color(0.75f, 1f, 1f, 1f);
    }

    public void ProntoUI() {
        pronto.SetActive(true);
        playerProperties["tapronto"] = true;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void NaoProntoUI() {
        pronto.SetActive(false);
        playerProperties["tapronto"] = false;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate (Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps) {
        if (player == targetPlayer  && changedProps.ContainsKey("tapronto")) {
            UpdatePlayerItem(targetPlayer);
        }
    }

    void UpdatePlayerItem (Player player) {
        if (player.CustomProperties.ContainsKey("tapronto")) {
            pronto.SetActive((bool)player.CustomProperties["tapronto"]);
        }
    }
}
