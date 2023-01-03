using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestruirMultiplayer : MonoBehaviour
{
    float esperar;
    PhotonView view;

    void Start() {
        view = GetComponent<PhotonView>();
    }

    public void DestruaApos (float tempo) {
        esperar = tempo;
        StartCoroutine("EsperaeDestrua");
    }

    IEnumerator EsperaeDestrua() {
        yield return new WaitForSeconds(esperar);

        if (view.IsMine)
            PhotonNetwork.Destroy(gameObject);
    }
}
