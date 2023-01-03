using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class PlayerSpawnMultiplayer : MonoBehaviourPunCallbacks
{
    public Transform[] spawnpoints;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    bool respawn = false;
    Transform pontodespawn;
    GameObject meuBoneco;

    void Awake()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        pontodespawn = spawnpoints[PhotonNetwork.LocalPlayer.GetPlayerNumber()];

        switch ((int)PhotonNetwork.LocalPlayer.CustomProperties["personagem"])
        {
            case 4: meuBoneco = PhotonNetwork.Instantiate("TessaMulti", pontodespawn.position, Quaternion.identity); break;
            default: break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            respawn = !respawn;
            playerProperties["respawn"] = respawn;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        }
    }

    public override void OnPlayerPropertiesUpdate (Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps) {
        if (targetPlayer.CustomProperties.ContainsKey("respawn") && changedProps.ContainsKey("respawn")) {
            Reiniciar();
        }
    }

    void Reiniciar() {
        PhotonNetwork.Destroy(meuBoneco);
        FindObjectOfType<Ativador>().ReiniciaTudo();
        FindObjectOfType<Energy>().QuantidadeCerta(10);
        pontodespawn = spawnpoints[PhotonNetwork.LocalPlayer.GetPlayerNumber()];

        switch ((int)PhotonNetwork.LocalPlayer.CustomProperties["personagem"])
        {
            case 4: meuBoneco = PhotonNetwork.Instantiate("TessaMulti", pontodespawn.position, Quaternion.identity); break;
            default: break;
        }
    }
}
