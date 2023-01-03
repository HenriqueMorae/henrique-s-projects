using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    [Header("Speed")]
    public float moveSpeedXForward;
    public float moveSpeedXBackwards;
    public float moveSpeedXNeutral;
    public float moveSpeedY;

    [Header("Car")]
    public Rigidbody2D rb;
    
    Vector2 movement;
    Coroutine turbo;
    float mSXF;
    float mSXB;
    float mSXN;
    float mSY;
    bool finish;
    bool ativado;

    void Awake() {
        moveSpeedXForward = FindObjectOfType<GameManager>().GetVelocidadeAcelera();
        moveSpeedY = FindObjectOfType<GameManager>().GetVelocidadeDirecao();
    }

    void Start () {
        finish = false;
        ativado = false;
        mSXF = moveSpeedXForward;
        mSXB = moveSpeedXBackwards;
        mSXN = moveSpeedXNeutral;
        mSY = moveSpeedY;
    }

    public void Finish() {
        finish = true;
    }

    public void OnMove(InputValue value)
	{
        if (finish) return;
	    movement = value.Get<Vector2>();
	}

    public void OnBoost(InputValue value)
    {
        if (value.isPressed && !finish) {
            if (turbo != null) {
                moveSpeedXForward = mSXF;
                moveSpeedXBackwards = mSXB;
                moveSpeedXNeutral = mSXN;
                moveSpeedY = mSY;
                StopCoroutine(turbo);
            }
            turbo = StartCoroutine(Boost());
        } else if (value.isPressed && finish) {
            if (turbo != null) StopCoroutine(turbo);
            turbo = StartCoroutine(BoostFinal());
        }
    }

    IEnumerator BoostFinal() {
        ativado = true;
        yield return new WaitForSeconds(0.1f);
        ativado = false;
    }

    IEnumerator Boost() {
        moveSpeedXForward *= 6;
        moveSpeedXBackwards *= 3;
        moveSpeedXNeutral *= -6;
        moveSpeedY *= 3;
        yield return new WaitForSeconds(TempoBoost());
        moveSpeedXForward = mSXF;
        moveSpeedXBackwards = mSXB;
        moveSpeedXNeutral = mSXN;
        moveSpeedY = mSY;
    }

    float TempoBoost() {
        if (FindObjectOfType<GameManager>().GetTurboDobrado()) {
            return 2*FindObjectOfType<RaceManager>().Boost()/50f;
        } else {
            return FindObjectOfType<RaceManager>().Boost()/50f;
        }
    }

    void FixedUpdate()
    {
        Vector2 mover;

        if (!finish) {
            mover = Vector2.ClampMagnitude(movement, 1f) * Time.fixedDeltaTime;

            if (mover.x > 0) mover.x *= moveSpeedXForward;
            else if (mover.x < 0) mover.x *= moveSpeedXBackwards;
            else if (mover.x == 0) mover.x = moveSpeedXNeutral * Time.fixedDeltaTime;

            mover.y *= moveSpeedY;
        } else {
            mover = new Vector2(0.75f,0) * Time.fixedDeltaTime;

            if (ativado) mover.x *= FindObjectOfType<RaceManager>().NumeroSC();
            else mover.x *= -5;
        }

        rb.MovePosition(rb.position + mover);
    }
}
