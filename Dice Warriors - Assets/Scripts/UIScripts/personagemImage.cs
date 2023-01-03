using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personagemImage : MonoBehaviour
{

    public SpriteRenderer personagem;

    public Sprite d4;
    public Sprite d6;
    public Sprite d8;
    public Sprite d12;
    public Sprite d20;

    public void Tessa() {
        personagem.sprite = d4;
    }

    public void Calebe() {
        personagem.sprite = d6;
    }

    public void Octavia() {
        personagem.sprite = d8;
    }

    public void Dominick() {
        personagem.sprite = d12;
    }

    public void Icaro() {
        personagem.sprite = d20;
    }
}
