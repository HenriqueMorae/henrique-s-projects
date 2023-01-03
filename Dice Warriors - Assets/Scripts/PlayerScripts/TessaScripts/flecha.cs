using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class flecha : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public Animator animator;

    int dano;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        view = GetComponent<PhotonView>();
    }

    void OnTriggerEnter2D (Collider2D hitinfo)
    {
        if (view != null) {
            DanoMultiplayer(hitinfo);
            return;
        }

        if (hitinfo.tag == "Player" || hitinfo.tag == "Flecha") {
        } else {
            Enemy inimigo = hitinfo.GetComponent<Enemy>();

            if (inimigo != null) {
                dano = Random.Range(1, 5);
                inimigo.LevarDano(dano+EfeitosDown.danoExtra);
            }

            rb.velocity = transform.right * 0f;
            animator.Play("hit");
            //Destroy(gameObject);
        }
    }

    void DanoMultiplayer (Collider2D hitinfo) {
        if (view.IsMine) {
            if (hitinfo.tag == "Flecha")
                return;
            
            if (hitinfo.tag == "Player" && hitinfo.gameObject.GetComponent<PhotonView>().IsMine)
                return;

            if (hitinfo.tag == "Player" && !hitinfo.gameObject.GetComponent<PhotonView>().IsMine) {
                if (PlayerPrefs.GetInt("multiplayer_modo") == 1) {
                    dano = Random.Range(1, 5);
                    hitinfo.gameObject.GetComponent<Vida>().LevarDanoMultiplayer(dano+EfeitosDown.danoExtra);
                    
                    rb.velocity = transform.right * 0f;
                    animator.Play("hit");
                }
            } else {
                rb.velocity = transform.right * 0f;
                animator.Play("hit");
            }   
        } else {
            if (hitinfo.tag == "Flecha")
                return;

            if (hitinfo.tag == "Player" && hitinfo.gameObject.GetComponent<PhotonView>().Owner == view.Owner)
                return;
            
            if (hitinfo.tag == "Player" && hitinfo.gameObject.GetComponent<PhotonView>().Owner != view.Owner) {
                if (PlayerPrefs.GetInt("multiplayer_modo") == 1) {
                    rb.velocity = transform.right * 0f;
                    animator.Play("hit");
                }
            } else {
                rb.velocity = transform.right * 0f;
                animator.Play("hit");
            }
        }
    }
}
