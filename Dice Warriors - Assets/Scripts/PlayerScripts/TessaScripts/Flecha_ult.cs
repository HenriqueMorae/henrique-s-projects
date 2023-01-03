using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Flecha_ult : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public Animator animator;
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
                inimigo.LevarDano(4+EfeitosDown.danoExtra);
            }

            rb.velocity = transform.right * 0f;
            animator.Play("hit_ult");
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
                    hitinfo.gameObject.GetComponent<Vida>().LevarDanoMultiplayer(4+EfeitosDown.danoExtra);
                    
                    rb.velocity = transform.right * 0f;
                    animator.Play("hit_ult");
                }
            } else {
                rb.velocity = transform.right * 0f;
                    animator.Play("hit_ult");
            }   
        } else {
            if (hitinfo.tag == "Flecha")
                return;

            if (hitinfo.tag == "Player" && hitinfo.gameObject.GetComponent<PhotonView>().Owner == view.Owner)
                return;
            
            if (hitinfo.tag == "Player" && hitinfo.gameObject.GetComponent<PhotonView>().Owner != view.Owner) {
                if (PlayerPrefs.GetInt("multiplayer_modo") == 1) {
                    rb.velocity = transform.right * 0f;
                    animator.Play("hit_ult");
                }
            } else {
                rb.velocity = transform.right * 0f;
                animator.Play("hit_ult");
            }
        }
    }
}
