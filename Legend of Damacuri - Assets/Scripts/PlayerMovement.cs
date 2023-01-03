using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerClass
{
    Jaguar,
    Tortoise
} 

public class PlayerMovement : MonoBehaviour
{
    public static event Action<Transform> OnPlayerInstantiated;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform attackPoint;
    [SerializeField] Animator animator;

    Vector2 movement;
    float moveSpeedAtual;
    float dashSpeed;
    float dashDuration;
    float attackPointOffset;

    void Start() {
        OnPlayerInstantiated?.Invoke(transform);
        moveSpeedAtual = moveSpeed;
        if (attackPoint != null) attackPointOffset = attackPoint.localPosition.x;
    }

    public void OnMove(InputValue value)
	{
	    movement = value.Get<Vector2>();
	}

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector2.ClampMagnitude(movement, 1f) * moveSpeedAtual * Time.fixedDeltaTime);

        animator.SetFloat("running", movement.sqrMagnitude);

        if (attackPoint != null) {
            if (movement.x > 0) {
                attackPoint.localPosition = new Vector2(attackPointOffset, 0f);
                attackPoint.localEulerAngles = new Vector3(0f,0f,0f);
                animator.SetTrigger("right");
            } else if (movement.x < 0) {
                attackPoint.localPosition = new Vector2(-attackPointOffset, 0f);
                attackPoint.localEulerAngles = new Vector3(0f,0f,180f);
                animator.SetTrigger("left");
            } else if (movement.y > 0) {
                attackPoint.localPosition = new Vector2(0f, attackPointOffset);
                attackPoint.localEulerAngles = new Vector3(0f,0f,90f);
                animator.SetTrigger("up");
            } else if (movement.y < 0) {
                attackPoint.localPosition = new Vector2(0f, -attackPointOffset);
                attackPoint.localEulerAngles = new Vector3(0f,0f,-90f);
                animator.SetTrigger("down");
            }
        }

        if (movement.sqrMagnitude > 0) {
            if (!FindObjectOfType<AudioManager>().IsPlaying("char_walking")) {
                FindObjectOfType<AudioManager>().Play("char_walking");
            }
        } else {
            FindObjectOfType<AudioManager>().Stop("char_walking");
        }
    }

    public void Dash(float dashSpeed, float dashDuration) {
        FindObjectOfType<AudioManager>().Play("weapon_attack");
        this.dashSpeed = dashSpeed;
        this.dashDuration = dashDuration;
        StartCoroutine(DashCoroutine());
    }

    IEnumerator DashCoroutine() {
        moveSpeedAtual = dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        moveSpeedAtual = moveSpeed;
    }
}
