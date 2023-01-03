using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    public AudioSource fala;

    public AudioClip moeda1;
    public AudioClip moeda2;
    public AudioClip moeda3;
    public AudioClip moeda4;
    public AudioClip moeda5;
    public AudioClip moeda6;
    public AudioClip moeda7;

    public AudioClip player1;
    public AudioClip player2;
    public AudioClip player3;
    public AudioClip player4;
    public AudioClip player5;
    public AudioClip player6;
    public AudioClip player7;
    public AudioClip player8;
    public AudioClip player9;
    public AudioClip player10;

    public AudioClip power11;
    public AudioClip power12;
    public AudioClip power13;
    public AudioClip power21;
    public AudioClip power22;
    public AudioClip power23;
    public AudioClip power31;
    public AudioClip power32;
    public AudioClip power33;
    public AudioClip ult1;
    public AudioClip ult2;
    public AudioClip ult3;

    public AudioClip dano1;
    public AudioClip dano2;
    public AudioClip dano3;
    public AudioClip dano4;
    public AudioClip dano5;

    public AudioClip cura1;
    public AudioClip cura2;
    public AudioClip cura3;

    void Start() {
        fala.volume = PlayerPrefs.GetFloat("voice", 0.8f);
    }

    public void ParaFala() {
        int numero = Random.Range(1, 6);

        switch (numero)
        {
            case 1: fala.clip = dano1; break;
            case 2: fala.clip = dano2; break;
            case 3: fala.clip = dano3; break;
            case 4: fala.clip = dano4; break;
            case 5: fala.clip = dano5; break;
        }

        fala.Play();

        StartCoroutine("Parou");
    }

    IEnumerator Parou() {
        yield return new WaitForSeconds(0.8f);
        fala.mute = true;
    }

    public void MudaAgora()
    {
        fala.volume = PlayerPrefs.GetFloat("voice");
    }

    public void FalaFala()
    {
        int numero = Random.Range(1, 18);

        switch (numero)
        {
            case 1: fala.clip = moeda1; break;
            case 2: fala.clip = moeda2; break;
            case 3: fala.clip = moeda3; break;
            case 4: fala.clip = moeda4; break;
            case 5: fala.clip = moeda5; break;
            case 6: fala.clip = moeda6; break;
            case 7: fala.clip = moeda7; break;
            case 8: fala.clip = player1; break;
            case 9: fala.clip = player2; break;
            case 10: fala.clip = player3; break;
            case 11: fala.clip = player4; break;
            case 12: fala.clip = player5; break;
            case 13: fala.clip = player6; break;
            case 14: fala.clip = player7; break;
            case 15: fala.clip = player8; break;
            case 16: fala.clip = player9; break;
            case 17: fala.clip = player10; break;
        }

        fala.Play();
    }

    public void Power1()
    {
        int tocar = Random.Range(1, 3);
        if (tocar == 2)
            return;

        int numero = Random.Range(1, 4);

        switch (numero)
        {
            case 1: fala.clip = power11; break;
            case 2: fala.clip = power12; break;
            case 3: fala.clip = power13; break;
        }

        fala.Play();
    }

    public void Power2()
    {
        int tocar = Random.Range(1, 3);
        if (tocar == 2)
            return;

        int numero = Random.Range(1, 4);

        switch (numero)
        {
            case 1: fala.clip = power21; break;
            case 2: fala.clip = power22; break;
            case 3: fala.clip = power23; break;
        }

        fala.Play();
    }

    public void Power3()
    {
        int tocar = Random.Range(1, 4);
        if (tocar == 2)
            return;

        int numero = Random.Range(1, 4);

        switch (numero)
        {
            case 1: fala.clip = power31; break;
            case 2: fala.clip = power32; break;
            case 3: fala.clip = power33; break;
        }

        fala.Play();
    }

    public void UltPronta()
    {
        int tocar = Random.Range(1, 4);
        if (tocar == 2)
            return;

        int numero = Random.Range(1, 4);

        switch (numero)
        {
            case 1: fala.clip = ult1; break;
            case 2: fala.clip = ult2; break;
            case 3: fala.clip = ult3; break;
        }

        fala.PlayDelayed(1f);
    }

    public void Dano()
    {
        int tocar = Random.Range(1, 5);
        if (tocar == 2)
            return;

        int numero = Random.Range(1, 6);

        switch (numero)
        {
            case 1: fala.clip = dano1; break;
            case 2: fala.clip = dano2; break;
            case 3: fala.clip = dano3; break;
            case 4: fala.clip = dano4; break;
            case 5: fala.clip = dano5; break;
        }

        fala.Play();
    }

    public void Cura()
    {
        int tocar = Random.Range(1, 5);
        if (tocar == 2)
            return;

        int numero = Random.Range(1, 4);

        switch (numero)
        {
            case 1: fala.clip = cura1; break;
            case 2: fala.clip = cura2; break;
            case 3: fala.clip = cura3; break;
        }

        fala.Play();
    }
}
