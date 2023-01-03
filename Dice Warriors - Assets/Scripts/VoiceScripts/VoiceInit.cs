using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceInit : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        fala.volume = PlayerPrefs.GetFloat("voice", 0.8f);

        int numero = Random.Range(1, 11);

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
        }

        fala.PlayDelayed(5f);
    }

    public void MudaAgora()
    {
        fala.volume = PlayerPrefs.GetFloat("voice");
    }
}
