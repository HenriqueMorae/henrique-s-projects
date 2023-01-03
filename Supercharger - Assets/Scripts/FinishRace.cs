using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishRace : MonoBehaviour
{
    public bool primeiraMissao;
    bool ganhou;

    void Start() {
        ganhou = false;
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Carro") {
            GameObject.FindWithTag("Limits").SetActive(false);
            FindObjectOfType<CarMovement>().enabled = false;
            FindObjectOfType<BackgroundCenario>().ParaTudo();

            if (other.GetComponent<CarMovement>() != null) ganhou = true;

            foreach (Rigidbody2D rb in FindObjectsOfType<Rigidbody2D>())
            {
                if (rb.tag == "Cenário") {
                    rb.velocity = new Vector2(0, 0);
                }

                if (rb.tag == "Carro") {
                    if (rb.GetComponent<Adversario>() != null) rb.GetComponent<Adversario>().enabled = false;
                    rb.velocity = new Vector2(5f, 0);
                    Destroy(rb.gameObject, 5f);
                    StartCoroutine(Resultado());
                }
            }

            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            FindObjectOfType<RaceManager>().Fim();
        }
    }

    IEnumerator Resultado() {
        yield return new WaitForSeconds(3f);
        if (primeiraMissao) {
            if (FindObjectOfType<RaceManager>().NumeroSC() >= 3) FindObjectOfType<RaceManager>().Completou();
            else FindObjectOfType<RaceManager>().Falha();
        } else {
            if (ganhou) FindObjectOfType<RaceManager>().Completou();
            else FindObjectOfType<RaceManager>().Falha();
        }
    }
}
