using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Flecha_explosiva : MonoBehaviour
{
    public bool multiplayer = false;
    PhotonView view;

    public float speed = 20f;
    public GameObject explosaoPrefab;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D (Collider2D hitinfo)
    {
        if (view != null) {
            Explosion(hitinfo);
            return;
        }

        if (hitinfo.tag == "Player" || hitinfo.tag == "Flecha") {
        } else {
            rb.velocity = transform.right * 0f;
            Muda("ExplosãoMulti", explosaoPrefab, transform.position, Quaternion.identity);
        }
    }

    void Explosion (Collider2D hitinfo) {
        if (view.IsMine) {
            if (hitinfo.tag == "Flecha")
                return;
            
            if (hitinfo.tag == "Player" && hitinfo.gameObject.GetComponent<PhotonView>().IsMine)
                return;

            rb.velocity = transform.right * 0f;
            Muda("ExplosãoMulti", explosaoPrefab, transform.position, Quaternion.identity);
        }
    }

    void Muda (string nomedotroco, Object prefab, Vector3 posicao, Quaternion rotacao) {
        if (multiplayer) {
            PhotonNetwork.Instantiate(nomedotroco, posicao, rotacao);

            if (view.IsMine)
                PhotonNetwork.Destroy(gameObject);
        } else {
            Instantiate(prefab, posicao, rotacao);
            Destroy(gameObject);
        }
    }
}
