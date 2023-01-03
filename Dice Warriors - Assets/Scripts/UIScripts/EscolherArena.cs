using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscolherArena : MonoBehaviour
{
    public Image imagem;

    public Sprite arena1;
    public Sprite arena2;
    public Sprite arena3;   

    public void Tradicional () {
        PlayerPrefs.SetString("arena", "tradicional");
    }

    public void Caixas () {
        PlayerPrefs.SetString("arena", "caixas");
    }

    public void Cercas () {
        PlayerPrefs.SetString("arena", "cercas");
    }

    public void TradicionalImage () {
        imagem.sprite = arena1;
        imagem.color = Color.white;
    }

    public void CaixasImage () {
        imagem.sprite = arena2;
        imagem.color = Color.white;
    }

    public void CercasImage () {
        imagem.sprite = arena3;
        imagem.color = Color.white;
    }

    public void Sumir () {
        imagem.color = Color.clear;
    }
}
