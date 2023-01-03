using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ExplosionDamage : MonoBehaviour
{
    public Transform center;
    public float range = 1f;
    public LayerMask layers;
    public int danoMin;
    public int danoMax;

    int dano;
    PhotonView view;

    void Awake () {
        FindObjectOfType<AudioManager>().Play("Explosao");
        view = GetComponent<PhotonView>();
    }

    public void Ataque () {
        if (view != null) {
            AtaqueMultiplayer();
            return;
        }

        //Detect in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(center.position, range, layers);

        //Damage
        foreach (Collider2D target in hits)
        {
            dano = Random.Range(danoMin, danoMax);
            
            if (target.tag == "Player")
                target.GetComponent<Vida>().LevarDano(dano+EfeitosDown.danoExtra);
            else
            {
                Enemy inimigo = target.GetComponent<Enemy>();
                
                if (inimigo != null) {
                    inimigo.LevarDano(dano+EfeitosDown.danoExtra);
                }
            }
        }
    }

    void AtaqueMultiplayer() {
        if (view.IsMine) {
            Collider2D[] hits = Physics2D.OverlapCircleAll(center.position, range, layers);

            foreach (Collider2D target in hits)
            {
                dano = Random.Range(danoMin, danoMax);

                if (target.tag == "Player")
                    target.GetComponent<Vida>().LevarDanoMultiplayer(dano+EfeitosDown.danoExtra);
            }
        }
    }

    void OnDrawGizmosSelected () {
        if (center == null)
            return;

        Gizmos.DrawWireSphere(center.position, range);
    }
}
