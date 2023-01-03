using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangevoiceGameover : MonoBehaviour
{
    public AudioSource fala;
    public AudioSource musica;

    public AudioClip nada1;
    public AudioClip bronze1;
    public AudioClip bronze2;
    public AudioClip prata1;
    public AudioClip ouro1;
    public AudioClip ouro2;

    public AudioClip nada2;
    public AudioClip nada3;
    public AudioClip bronze3;
    public AudioClip bronze4;
    public AudioClip prata2;
    public AudioClip prata3;
    public AudioClip ouro3;
    public AudioClip ouro4;

    int pontos;

    public void Vai()
    {
        fala.volume = PlayerPrefs.GetFloat("voice", 0.8f);

        pontos = PlayerPrefs.GetInt("placar");

        if (pontos < 50) {
            Nada();
        } else if (pontos >= 50 && pontos < 100) {
            Bronze();
        } else if (pontos >= 100 && pontos < 150) {
            Prata();
        } else if (pontos >= 150) {
            Ouro();
        }

        musica.PlayDelayed(4.5f);
    }

    void Nada() {
        int numero = Random.Range(1, 4);

        switch (numero)
        {
            case 1: fala.clip = nada1; break;
            case 2: fala.clip = nada2; break;
            case 3: fala.clip = nada3; break;
        }

        fala.Play();
    }

    void Bronze() {
        int numero = Random.Range(1, 5);

        switch (numero)
        {
            case 1: fala.clip = bronze1; break;
            case 2: fala.clip = bronze2; break;
            case 3: fala.clip = bronze3; break;
            case 4: fala.clip = bronze4; break;
        }

        fala.Play();
    }

    void Prata() {
        int numero = Random.Range(1, 4);

        switch (numero)
        {
            case 1: fala.clip = prata1; break;
            case 2: fala.clip = prata2; break;
            case 3: fala.clip = prata3; break;
        }

        fala.Play();
    }

    void Ouro() {
        int numero = Random.Range(1, 5);

        switch (numero)
        {
            case 1: fala.clip = ouro1; break;
            case 2: fala.clip = ouro2; break;
            case 3: fala.clip = ouro3; break;
            case 4: fala.clip = ouro4; break;
        }

        fala.Play();
    }
}
