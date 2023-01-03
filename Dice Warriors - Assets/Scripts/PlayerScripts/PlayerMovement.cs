using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class PlayerMovement : MonoBehaviour
{
    public bool multiplayer = false;

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public Vida vida;

    Vector2 movement;
    bool facingRight;
    AudioSource passos;
    string eixoHorizontal;
    string eixoVertical;

    PhotonView view;
    float correndo;

    void Start ()
    {
        passos = GetComponent<AudioSource>();
        view = GetComponent<PhotonView>();

        facingRight = true;

        if (multiplayer) {
            if (PhotonNetwork.LocalPlayer.GetPlayerNumber() == 1 || PhotonNetwork.LocalPlayer.GetPlayerNumber() == 3) {
                facingRight = !facingRight;
                transform.Rotate(0f, 180f, 0f);
            }
        }

        if (view != null) {
            if (!view.IsMine) {
                return;
            }
        }

        switch (PlayerPrefs.GetInt("modobotoes", 1))
        {
            case 1: eixoHorizontal = "Horizontal"; eixoVertical = "Vertical"; break;
            case 2: eixoHorizontal = "Horizontal"; eixoVertical = "Vertical"; break;
            case 3: eixoHorizontal = "Horizontal2"; eixoVertical = "Vertical2"; break;
            case 4: eixoHorizontal = "Horizontal2"; eixoVertical = "Vertical2"; break;
            case 5: eixoHorizontal = "Horizontal3"; eixoVertical = "Vertical3"; break;
            case 6: eixoHorizontal = "Horizontal3"; eixoVertical = "Vertical3"; break;
            case 7: eixoHorizontal = "HorizontalGamepad"; eixoVertical = "VerticalGamepad"; break;
            default: eixoHorizontal = "Horizontal"; eixoVertical = "Vertical"; break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (view != null) {
            if (!view.IsMine) {
                correndo = animator.GetFloat("running");

                if (correndo > 0 && !vida.Morreu()) {
                    if (!passos.isPlaying) {
                        passos.Play();
                    }
                } else {
                    passos.Stop();
                }

                return;
            }
        }

        if(PauseMenu.GameIsPaused)
            return;

        // Input
        movement.x = Input.GetAxisRaw(eixoHorizontal);
        movement.y = Input.GetAxisRaw(eixoVertical);

        animator.SetFloat("running", movement.sqrMagnitude);

        if (movement.sqrMagnitude > 0 && !vida.Morreu()) {
            if (!passos.isPlaying) {
                passos.Play();
            }
        } else {
            passos.Stop();
        }
    }

    void FixedUpdate()
    {
        if (view != null) {
            if (!view.IsMine) {
                return;
            }
        }

        if(PauseMenu.GameIsPaused)
            return;

        // Movement
        rb.MovePosition(rb.position + Vector2.ClampMagnitude(movement, 1f) * moveSpeed * Time.fixedDeltaTime);

        if (movement.x > 0 && !facingRight || movement.x < 0 && facingRight) {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    public Vector2 LadoCerto() {
        if (facingRight)
            return Vector2.right;
        else
            return Vector2.left;
    }

    public void ModificarVelocidade(float mod) {
        moveSpeed += mod;
    }
}
