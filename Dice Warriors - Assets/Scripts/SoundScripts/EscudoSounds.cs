using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscudoSounds : MonoBehaviour
{
    public AudioSource efeito;
    public AudioClip ligado;
    public AudioClip desligado;

    public void EscudoLigado() {
        efeito.clip = ligado;
        efeito.Play();
    }

    public void EscudoDesligado() {
        efeito.clip = desligado;
        efeito.Play();
    }
}
