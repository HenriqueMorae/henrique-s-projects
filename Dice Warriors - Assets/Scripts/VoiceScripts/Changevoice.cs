using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changevoice : MonoBehaviour
{
    public AudioSource fala;

    public AudioClip moeda1;
    public AudioClip moeda2;
    public AudioClip moeda3;
    public AudioClip tessa1;
    public AudioClip tessa2;
    public AudioClip tessa3;
    public AudioClip calebe1;
    public AudioClip calebe2;
    public AudioClip calebe3;
    public AudioClip octavia1;
    public AudioClip octavia2;
    public AudioClip octavia3;
    public AudioClip dominick1;
    public AudioClip dominick2;
    public AudioClip dominick3;
    public AudioClip icaro1;
    public AudioClip icaro2;
    public AudioClip icaro3;

    // Start is called before the first frame update
    void Start()
    {
        fala.volume = PlayerPrefs.GetFloat("voice", 0.8f);
    }

    public void Moeda() {
        int numero = Random.Range(1, 4);

        switch (numero)
        {
            case 1: fala.clip = moeda1; break;
            case 2: fala.clip = moeda2; break;
            case 3: fala.clip = moeda3; break;
        }

        fala.Play();
    }

    public void Tessa() {
        int numero = Random.Range(1, 4);

        switch (numero)
        {
            case 1: fala.clip = tessa1; break;
            case 2: fala.clip = tessa2; break;
            case 3: fala.clip = tessa3; break;
        }

        fala.Play();
    }

    public void Calebe() {
        int numero = Random.Range(1, 4);

        switch (numero)
        {
            case 1: fala.clip = calebe1; break;
            case 2: fala.clip = calebe2; break;
            case 3: fala.clip = calebe3; break;
        }

        fala.Play();
    }

    public void Octavia() {
        int numero = Random.Range(1, 4);

        switch (numero)
        {
            case 1: fala.clip = octavia1; break;
            case 2: fala.clip = octavia2; break;
            case 3: fala.clip = octavia3; break;
        }

        fala.Play();
    }

    public void Dominick() {
        int numero = Random.Range(1, 4);

        switch (numero)
        {
            case 1: fala.clip = dominick1; break;
            case 2: fala.clip = dominick2; break;
            case 3: fala.clip = dominick3; break;
        }

        fala.Play();
    }

    public void Icaro() {
        int numero = Random.Range(1, 4);

        switch (numero)
        {
            case 1: fala.clip = icaro1; break;
            case 2: fala.clip = icaro2; break;
            case 3: fala.clip = icaro3; break;
        }

        fala.Play();
    }
}
