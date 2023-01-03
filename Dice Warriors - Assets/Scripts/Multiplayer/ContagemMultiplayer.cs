using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ContagemMultiplayer : MonoBehaviourPunCallbacks
{
    public Text texto;
    public Animator efeito;
    public GameObject carregando;

    bool tacontandotabb;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    void Start()
    {
        tacontandotabb = false;
        carregando.SetActive(true);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        PauseMenu.GameIsPaused = false;

        playerProperties["nivelcarregado"] = true;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    void Update() {
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        foreach (KeyValuePair<int,Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            if (player.Value.CustomProperties.ContainsKey("nivelcarregado")) {
                if ((bool)player.Value.CustomProperties["nivelcarregado"]) {

                } else {
                    return;
                }
            } else {
                return;
            }
        }

        if (!tacontandotabb) {
            tacontandotabb = true;
            playerProperties["contagem"] = true;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        }
    }

    public override void OnPlayerPropertiesUpdate (Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps) {
        if (targetPlayer.CustomProperties.ContainsKey("contagem") && changedProps.ContainsKey("contagem")) {
            IniciarContagem();
        }
    }

    void IniciarContagem() {
        carregando.SetActive(false);
        texto.text = "3";
        FindObjectOfType<AudioManager>().Play("countdown");
        StartCoroutine("contar");
    }

    IEnumerator contar() {
        yield return new WaitForSeconds(1f);
        texto.text = "2";
        yield return new WaitForSeconds(1f);
        texto.text = "1";
        yield return new WaitForSeconds(1f);
        texto.text = "LUTE!";
        FindObjectOfType<UDEP>().Energizando();
        efeito.Play("contagem_1");
    }
}
