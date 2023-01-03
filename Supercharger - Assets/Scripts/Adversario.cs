using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adversario : MonoBehaviour
{
    [Header("Dodge")]
    public float safetyRange;
    public float limitsRange;
    public float carroRange;
    public LayerMask obstaculoLayer;
    public LayerMask limitesLayer;
    public LayerMask carroLayer;

    [Header("Speed")]
    public float moveSpeedXForward;
    public float moveSpeedXBackwards;
    public float moveSpeedXFinal;
    public float moveSpeedY;

    [Header("Car")]
    public Rigidbody2D rb;

    float menorDistancia;
    Transform maisPerto;
    float proximoX;
    bool final;
    float moverX;
    float moverY;
    bool chegueiDeCima;
    bool chegueiDeBaixo;
    bool fugindoDeObsSemLimites;

    void Start() {
        StartCoroutine(Andando());
        final = false;
        fugindoDeObsSemLimites = false;
        moverX = 0;
        moverY = 0;
    }

    IEnumerator Andando() {
        while (!final)
        {
            proximoX = Random.Range(-5f, 6f);
            chegueiDeCima = false;
            chegueiDeBaixo = false;
            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }

    void Update()
    {
        DesviarObstaculos();
        Andar();
    }

    void Andar() {
        if (final) {
            moverX = Time.deltaTime * moveSpeedXFinal;
            rb.MovePosition(rb.position + new Vector2(moverX, moverY));
        } else {
            if (transform.position.x > proximoX && !chegueiDeCima) {
                chegueiDeBaixo = true;
                moverX = -1 * Time.deltaTime * moveSpeedXBackwards;
                rb.MovePosition(rb.position + new Vector2(moverX, moverY));
            } else if (transform.position.x < proximoX && !chegueiDeBaixo) {
                chegueiDeCima = true;
                moverX = Time.deltaTime * moveSpeedXForward;
                rb.MovePosition(rb.position + new Vector2(moverX, moverY));
            } else {
                moverX = 0;
                rb.MovePosition(rb.position + new Vector2(moverX, moverY));
            }
        }
    }

    public void Final() {
        final = true;
    }

    void DesviarObstaculos() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position + new Vector3(1.75f,0), safetyRange, obstaculoLayer);
        Collider2D[] hitsLimites = Physics2D.OverlapCircleAll(transform.position + new Vector3(1.75f,0), limitsRange, limitesLayer);
        Collider2D[] hitsCarros = Physics2D.OverlapCircleAll(transform.position + new Vector3(0,0), carroRange, carroLayer);
        menorDistancia = Mathf.Infinity;

        if (hitsCarros.Length > 1) {
            foreach (Collider2D hit in hitsCarros)
            {
                if (hit.gameObject != this.gameObject) {
                    maisPerto = hit.transform;
                }
            }

            if (maisPerto.position.y > transform.position.y) {
                moverY = -1 * Time.deltaTime * moveSpeedY;
            }

            if (maisPerto.position.y < transform.position.y) {
                moverY = Time.deltaTime * moveSpeedY;
            }

        } else if (hits.Length > 0) {
            if (hitsLimites.Length > 0 && !fugindoDeObsSemLimites) {
                if (hitsLimites[0].transform.position.y > transform.position.y) {
                    moverY = -1 * Time.deltaTime * moveSpeedY;
                }

                if (hitsLimites[0].transform.position.y < transform.position.y) {
                    moverY = Time.deltaTime * moveSpeedY;
                }

                return;
            }

            fugindoDeObsSemLimites = true;

            foreach (Collider2D hit in hits)
            {
                if (Vector2.Distance(transform.position, hit.transform.position) < menorDistancia) {
                    menorDistancia = Vector2.Distance(transform.position, hit.transform.position);
                    maisPerto = hit.transform;
                }
            }

            if (maisPerto.position.y > transform.position.y) {
                moverY = -1 * Time.deltaTime * moveSpeedY;
            }

            if (maisPerto.position.y < transform.position.y) {
                moverY = Time.deltaTime * moveSpeedY;
            }
        } else {
            moverY = 0;
            fugindoDeObsSemLimites = false;
        }
    }

    void OnDrawGizmosSelected () {
        Gizmos.DrawWireSphere(transform.position + new Vector3(1.75f,0), safetyRange);
        Gizmos.DrawWireSphere(transform.position + new Vector3(1.75f,0), limitsRange);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0,0), carroRange);
    }
}