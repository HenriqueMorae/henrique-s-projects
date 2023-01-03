using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int vida = 5;
    public int ptsEnergia = 1;
    public AIPath aiPath;
    public AIDestinationSetter target;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask playerLayer;
    public int qtdDados = 1;
    public int danoMin;
    public int danoMax;
    public float scale = 1f;
    public string somAtaque;
    public string qualAtaque;
    public SpriteRenderer cara;
    public Sprite dor;
    public Sprite normal;
    public Transform myPoint;
    public GameObject smokePrefab;

    GameObject energyBar;
    Energy barraDeEnergia;
    Vector2 movement;
    int dano;
    bool morreu;
    GameObject alvo;
    GameObject placar;
    Placar placarbar;
    GameObject esquerdaEmbaixo;
    LeftDown combo;

    void Start()
    {
        morreu = false;
        placar = GameObject.FindWithTag("Placar");
        placarbar = placar.GetComponent<Placar>();
        alvo = GameObject.FindWithTag("Player");
        energyBar = GameObject.FindWithTag("EnergyBar");
        barraDeEnergia = energyBar.GetComponent<Energy>();
        esquerdaEmbaixo = GameObject.FindWithTag("LeftDown");
        combo = esquerdaEmbaixo.GetComponent<LeftDown>();
        target.target = alvo.transform;
    }

    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.001f) {
            transform.localScale = new Vector3(scale, scale, scale);
        } else if (aiPath.desiredVelocity.x <= -0.001f) {
            transform.localScale = new Vector3(-scale, scale, scale);
        }

        movement.x = aiPath.desiredVelocity.x;
        movement.y = aiPath.desiredVelocity.y;

        if (aiPath.reachedDestination && movement.sqrMagnitude == 0) {
            animator.SetBool(qualAtaque, true);
        } else {
            animator.SetBool(qualAtaque, false);
        }

        animator.SetFloat("running", movement.sqrMagnitude);
    }

    public void LevarDano (int dano) {
        vida -= dano;
        StartCoroutine("SentindoDor");

        if (vida <= 0 && morreu == false) {
            Morte();
        }
    }

    IEnumerator SentindoDor() {
        cara.sprite = dor;
        yield return new WaitForSeconds(0.25f);
        cara.sprite = normal;
    }

    public void Smoke() {
        Instantiate(smokePrefab, myPoint.position, myPoint.rotation);
    }

    void Morte () {
        StopCoroutine("SentindoDor");
        morreu = true;
        combo.Abate();
        barraDeEnergia.Energia(ptsEnergia);
        placarbar.Maispontos(1);
        aiPath.maxSpeed = 0;
        cara.sprite = dor;
        animator.Play("d2_inimigo_death");
        //Destroy(gameObject);
    }

    public void Ataque () {
        //Detect player in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        //Damage
        foreach (Collider2D player in hits)
        {
            for (int i = 0; i < qtdDados; i++)
            {
                dano = Random.Range(danoMin, danoMax);
                player.GetComponent<Vida>().LevarDano(dano);   
            }
            FindObjectOfType<AudioManager>().Play(somAtaque);
        }
    }

    void OnDrawGizmosSelected () {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
