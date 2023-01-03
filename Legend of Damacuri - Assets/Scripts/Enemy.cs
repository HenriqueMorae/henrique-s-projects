using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [SerializeField] AIDestinationSetter target;
    [SerializeField] AIPath aiPath;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] int dano;
    [SerializeField] int hitPoints;
    [SerializeField] float attackCooldown;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer sr;

    bool isAttacking;
    bool morri;
    float nextTargetCheck;

    void Start() {
        isAttacking = false;
        morri = false;
        SearchTarget();
    }

    void Update() {
        if (morri) return;

        if (target.target == null)
        {
            if (Time.time > nextTargetCheck)
                SearchTarget();
            return;
        }
        else if (target.target.TryGetComponent(out Health health) && health.IsDead)
        {
            target.target = null;
            return;
        }

        if(target.target.position.x >= transform.position.x) {
            transform.localScale = new Vector3(1, 1, 1);
        } else if (target.target.position.x < transform.position.x) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (aiPath.reachedDestination && !isAttacking && !morri) {
            isAttacking = true;
            animator.SetBool("attack", true);
            StartCoroutine(Attack());
        }
    }

    private void SearchTarget ()
    {
        target.target = GameObject.FindWithTag("Player")?.transform;
        nextTargetCheck = Time.time + Random.Range(0.5f, 1f);
    }

    IEnumerator Attack() {
        while (aiPath.reachedDestination) {
            yield return new WaitForSeconds(0.2f);
            Ataque();
            yield return new WaitForSeconds(attackCooldown);
        }
        isAttacking = false;
        animator.SetBool("attack", false);
    }

    public void Dano(int damage) {
        if (morri) return;
        hitPoints -= damage;

        if (hitPoints <= 0 && !morri) {
            StopAllCoroutines();
            morri = true;
            animator.Play("Die");
            StartCoroutine(Morte());
        } else {
            StartCoroutine(Hit());
        }
    }

    IEnumerator Hit() {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        sr.color = Color.white;
    }

    IEnumerator Morte() {
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }

    public void Ataque () {
        //Detect player in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        //Damage
        foreach (Collider2D player in hits)
        {
            player.GetComponent<Health>().Dano(dano);
            FindObjectOfType<AudioManager>().Play("enemy_contact");
        }
    }

    void OnDrawGizmosSelected () {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
