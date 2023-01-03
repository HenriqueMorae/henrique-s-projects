using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Onca : MonoBehaviour
{
	[SerializeField] PlayerMovement pm;
	[SerializeField] Animator animator;
	[SerializeField] Collider2D cl;
	[SerializeField] Rigidbody2D rb;

	[Header("Ataque")]
	[SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask enemyLayer;
	[SerializeField] int danoAtaque;
	[SerializeField] float ataqueCooldown;

	[Header("Dash")]
	[SerializeField] float dashCooldown;
	[SerializeField] float dashSpeed;
	[SerializeField] float dashDuration;
	[SerializeField] int danoDash;
	[SerializeField] TrailRenderer tr;

	[Header("Efeito Soco")]
    [SerializeField] SpriteRenderer localEfeito;
    [SerializeField] Sprite[] frames;

	bool canDash;
	bool canAttack;

	void Start() {
		canDash = true;
		canAttack = true;
	}

    public void EfeitoPow() {
        Ataque();
        StartCoroutine(PowPow());
    }

    IEnumerator PowPow() {
        foreach (Sprite frame in frames)
        {
            localEfeito.sprite = frame;
            yield return new WaitForSeconds(0.1f);
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
	    if (value.isPressed && canDash) {
			canDash = false;
			tr.emitting = true;
			pm.Dash(dashSpeed, dashDuration);
			StartCoroutine(CooldownDash());
			StartCoroutine(DashAttack());
        }
	}

	IEnumerator DashAttack() {
		cl.isTrigger = true;
		yield return new WaitForSeconds(dashDuration);
		tr.emitting = false;
		cl.isTrigger = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Enemy") {
			other.GetComponent<Enemy>().Dano(danoDash);
		}
	}

	IEnumerator CooldownDash() {
		yield return new WaitForSeconds(dashCooldown);
		canDash = true;
	}

	IEnumerator AtaqueCooldown() {
		yield return new WaitForSeconds(ataqueCooldown);
		canAttack = true;
	}

    public void Ataque () {
		FindObjectOfType<AudioManager>().Play("char_punch_1");

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
        yield return new WaitForSeconds(0.5f);
        pm.enabled = true;
        animator.SetBool("pare", false);
    }

    void OnDrawGizmosSelected () {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
