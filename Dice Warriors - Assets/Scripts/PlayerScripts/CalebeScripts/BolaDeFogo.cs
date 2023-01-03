using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaDeFogo : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public float speed = 20f;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D target)
    {       
        if (target.tag == "Player")
        {

        }
        else if (target.tag == "Enemy")
        {
            Enemy inimigo = target.GetComponent<Enemy>();
            
            if (inimigo != null) {
                inimigo.LevarDano(6+EfeitosDown.danoExtra);
            }
        }
        else
        {
            rb.velocity = transform.right * 0f;
            animator.Play("boladefogo_hit");
        }
    }
}
