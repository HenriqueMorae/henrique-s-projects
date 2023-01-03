using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TremorEmpurra : MonoBehaviour
{
    GameObject jogador;
    Arma_D12 alvo;

    void Awake () {
        FindObjectOfType<AudioManager>().Play("Tremor");
    }

    void Start() {
        jogador = GameObject.FindWithTag("Player");
        alvo = jogador.GetComponent<Arma_D12>();
    }

    void OnTriggerEnter2D(Collider2D target) {
        alvo.UltEmpurrao(target);
    }

}
