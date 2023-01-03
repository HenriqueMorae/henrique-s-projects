using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = -1f;
    
    float rotation;
    bool bateu;
    float widthToDestroy;

    void Start() {
        widthToDestroy = -15f;
        rotation = Random.Range(1f,10f);
        bateu = false;
        rb.velocity = new Vector2(speed, 0f);
    }

    void Update() {
        if (transform.position.x < widthToDestroy) {
            Destroy(gameObject);
        }

        if (bateu) {
            transform.Rotate(new Vector3(0f,0f,rotation));
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Carro") {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            if (other.GetComponent<CarMovement>() != null) FindObjectOfType<RaceManager>().Dano(Random.Range(5,10));
            rb.velocity = new Vector2(0f, 0f);
            rb.AddForce(new Vector2(10f,Random.Range(-10f,10f)), ForceMode2D.Impulse);
            bateu = true;
            StartCoroutine("Pow");
            FindObjectOfType<AudioManager>().Play("bateuObs");
        }
    }

    IEnumerator Pow() {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
