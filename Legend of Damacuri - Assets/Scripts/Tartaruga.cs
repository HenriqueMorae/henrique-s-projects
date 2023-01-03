using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tartaruga : MonoBehaviour
{
    [SerializeField] Health hp;
    [SerializeField] Animator animator;
    [SerializeField] PlayerMovement pm;
    [SerializeField] Rigidbody2D rb;

	[Header("Ataque")]
	[SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask enemyLayer;
	[SerializeField] int danoAtaque;
	[SerializeField] float ataqueCooldown;

    [Header("Escudo")]
    [SerializeField] int vidaDoEscudo;
    [SerializeField] float duracaoEscudo;
    [SerializeField] float escudoCooldown;

    [Header("Efeito Soco")]
    [SerializeField] SpriteRenderer localEfeito;
    [SerializeField] Sprite[] frames;

    bool canAttack;
    bool canEscudo;

    void Start() {
		canAttack = true;
        canEscudo = true;
	}

    public void EfeitoPow() {
        Ataque();
        StartCoroutine(PowPow());
    }

    IEnumerator PowPow() {
        foreach (Sprite frame in frames)
        {
            localEfeito.sprite = frame;
            yield return new WaitForSeconds(0.15f);
        }
        localEfeito.sprite = null;
    }

    public void OnAttack(InputValue value)
	{
	    if (value.isPressed && canAttack) {
            canAttack = false;
			animator.SetTrigger("hit");
            StartCoroutine(PareESoque());
			StartCoroutine(AtaqueCooldown());
        }
	}

    public void OnPower1(InputValue value)
	{
	    if (value.isPressed && canEscudo) {
			canEscudo = false;
			StartCoroutine(CooldownEscudo());
			StartCoroutine(AtivarEscudo());
        }
	}

    IEnumerator AtivarEscudo() {
        hp.EscudoOn(vidaDoEscudo);
        yield return new WaitForSeconds(duracaoEscudo);
        hp.EscudoOff();
    }

    IEnumerator CooldownEscudo() {
	    yield return new WaitForSeconds(escudoCooldown);
	    canEscudo = true;
	}

    IEnumerator AtaqueCooldown() {
	    yield return new WaitForSeconds(ataqueCooldown);
	    canAttack = true;
	}

    public void Ataque () {
        FindObjectOfType<AudioManager>().Play("char_punch_2");

        //Detect player in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        //Damage
        foreach (Collider2D enemy in hits)
        {
            enemy.GetComponent<Enemy>().Dano(danoAtaque);
        }
    }

    IEnumerator PareESoque() {
        pm.enabled = false;
		rb.velocity = new Vector2(0f, 0f);
        FindObjectOfType<AudioManager>().Stop("char_walking");
        animator.SetBool("pare", true);
        yield return new WaitForSeconds(1f);
        pm.enabled = true;
        animator.SetBool("pare", false);
    }

    void OnDrawGizmosSelected () {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
